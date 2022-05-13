using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContactlessOrder.BLL.Infrastructure;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.DAL.Entities.Users;
using ContactlessOrder.DAL.Interfaces;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ContactlessOrder.DAL.Entities.Companies;

namespace ContactlessOrder.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IValidationService _validationService;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly EmailHelper _emailHelper;
        private readonly INotificationService _notificationService;
        private readonly string _secret;
        private readonly string _googleClientId;

        public AuthService(IUserRepository repository, IConfiguration configuration, IMapper mapper,
            EmailHelper emailHelper, IValidationService validationService, INotificationService notificationService)
        {
            _secret = configuration["AppSettings:Secret"];
            _googleClientId = configuration["GoogleAuthSettings:clientId"];
            _repository = repository;
            _mapper = mapper;
            _emailHelper = emailHelper;
            _validationService = validationService;
            _notificationService = notificationService;
        }

        public async Task<ResponseDto<string>> Authenticate(UserLoginRequestDto dto)
        {
            var passwordHash = CryptoHelper.GetMd5Hash(dto.Password);
            var user = await _repository.GetUser(dto.Email);

            if (user == null)
            {
                return new ResponseDto<string>() { ErrorMessage = "Користувач не знайдений" };
            }
            else if (user.PasswordHash != passwordHash)
            {
                return new ResponseDto<string>() { ErrorMessage = "Невірний пароль" };
            }
            else if (!user.EmailConfirmed)
            {
                return new ResponseDto<string>() { ErrorMessage = "Перевірте скриньку електронної пошти для підтвердження адреси" };
            }

            return new ResponseDto<string>() { Response = GenerateToken(user) };
        }

        public async Task<ResponseDto<string>> GoogleAuthenticate(GoogleRegisterRequestDto dto)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _googleClientId }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, settings);

                var user = await _repository.GetUser(payload.Email);

                if (user == null)
                {
                    var role = await _repository.Get<Role>(e => e.Value == UserRoles.ClientValue);

                    user = new User()
                    {
                        Email = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        PasswordHash = string.Empty,
                        PhoneNumber = dto.PhoneNumber,
                        ProfilePhotoPath = payload.Picture,
                        RegistrationDate = DateTime.UtcNow,
                        EmailConfirmed = true,
                        RoleId = role.Id,
                    };

                    await _repository.Add(user);
                    await _repository.SaveChanges();

                    user.Role = role;
                }

                return new ResponseDto<string>() { Response = GenerateToken(user) };
            }
            catch (Exception)
            {
                return new ResponseDto<string>() { ErrorMessage = "Невірна спроба зовнішньої автентифікації" };
            }
        }

        public async Task<ResponseDto<string>> Register(UserRegisterRequestDto dto)
        {
            var error = await _validationService.ValidateUser(dto);
            if (!string.IsNullOrEmpty(error))
            {
                return new ResponseDto<string>() { ErrorMessage = error };
            }

            var user = _mapper.Map<User>(dto);

            var role = await _repository.Get<Role>(e => e.Value == UserRoles.ClientValue);

            user.RoleId = role.Id;
            user.RegistrationDate = DateTime.UtcNow;
            user.EmailConfirmed = false;
            user.PasswordHash = CryptoHelper.GetMd5Hash(dto.Password);

            await _repository.Add(user);
            await _repository.SaveChanges();

            user.Role = role;

            await _emailHelper.SendConfirmEmail(user.Email, $"{user.FirstName} {user.LastName}", GenerateToken(user));

            return new ResponseDto<string>() { Response = "Посилання для підтвердження електронної пошти надіслано на вашу поштову адресу" };
        }

        public async Task<ResponseDto<string>> RegisterCompany(CompanyRegisterRequestDto dto)
        {
            var error = await _validationService.ValidateCompany(dto);
            if (!string.IsNullOrEmpty(error))
            {
                return new ResponseDto<string>() { ErrorMessage = error };
            }

            var role = await _repository.Get<Role>(e => e.Value == UserRoles.CompanyValue);

            var user = new User()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                PasswordHash = CryptoHelper.GetMd5Hash(dto.Password),
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                RegistrationDate = DateTime.UtcNow,
                EmailConfirmed = false,
                RoleId = role.Id,
                Company = new Company()
                {
                    Name = dto.Name,
                }
            };

            await _repository.Add(user);
            await _repository.SaveChanges();

            user.Role = role;

            await _emailHelper.SendConfirmEmail(user.Email, dto.Name, GenerateToken(user));

            return new ResponseDto<string>() { Response = "Посилання для підтвердження електронної пошти надіслано на вашу поштову адресу" };
        }

        public async Task<string> ConfirmEmail(int userId)
        {
            var user = await _repository.GetUser(userId);

            if (user == null)
            {
                return "Користувач не знайдений";
            }
            else if (user.EmailConfirmed)
            {
                return "Електронна адреса вже підтвердженна";
            }

            user.EmailConfirmed = true;
            await _repository.SaveChanges();

            if (user.Company != null)
            {
                await _notificationService.NotifyCompanyAdded(userId);
            }

            return string.Empty;
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var claims = new List<Claim>
            {
                new Claim(TokenProperties.Id, user.Id.ToString()),
                new Claim(TokenProperties.Email, user.Email),
                new Claim(TokenProperties.FullName, GetUserName(user)),
                new Claim(TokenProperties.Role, UserRoles.GetName(user.Role.Value)),
                new Claim(TokenProperties.RoleValue, user.Role.Value.ToString()),
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                     SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescription));
            return token;
        }

        private string GetUserName(User user)
        {
            return user.Company?.Name ?? user.Catering?.Name ?? $"{user.FirstName} {user.LastName}";
        }
    }
}
