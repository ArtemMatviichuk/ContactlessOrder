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
        private readonly string _secret;
        private readonly string _googleClientId;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly EmailHelper _emailHelper;

        public AuthService(IUserRepository repository, IConfiguration configuration, IMapper mapper, EmailHelper emailHelper)
        {
            _secret = configuration["AppSettings:Secret"];
            _googleClientId = configuration["GoogleAuthSettings:clientId"];
            _repository = repository;
            _mapper = mapper;
            _emailHelper = emailHelper;
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

        public async Task<ResponseDto<string>> GoogleLogin(GoogleRegisterRequestDto dto)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _googleClientId }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, settings);

                var user = await _repository.Get<User>(e => e.Email == payload.Email);

                if (user == null)
                {
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
                    };

                    await _repository.Add(user);
                    await _repository.SaveChanges();
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
            var error = await ValidateUser(dto);
            if (!string.IsNullOrEmpty(error))
            {
                return new ResponseDto<string>() { ErrorMessage = error };
            }

            var user = _mapper.Map<User>(dto);
            user.RegistrationDate = DateTime.UtcNow;
            user.EmailConfirmed = false;
            user.PasswordHash = CryptoHelper.GetMd5Hash(dto.Password);

            await _repository.Add(user);
            await _repository.SaveChanges();

            await _emailHelper.SendConfirmEmail(user.Email, $"{user.FirstName} {user.LastName}", GenerateToken(user));

            return new ResponseDto<string>() { Response = "Посилання для підтвердження електронної пошти надіслано на вашу поштову адресу" };
        }

        public async Task<ResponseDto<string>> RegisterCompany(CompanyRegisterRequestDto dto)
        {
            var error = await ValidateCompany(dto);
            if (!string.IsNullOrEmpty(error))
            {
                return new ResponseDto<string>() { ErrorMessage = error };
            }

            var dateTimeNow = DateTime.UtcNow;

            var user = new User()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                PasswordHash = CryptoHelper.GetMd5Hash(dto.Password),
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                RegistrationDate = dateTimeNow,
                EmailConfirmed = false,
                Company = new Company()
                {
                    Name = dto.Name,
                    RegisteredDate = dateTimeNow,
                }
            };

            await _repository.Add(user);
            await _repository.SaveChanges();

            await _emailHelper.SendConfirmEmail(user.Email, dto.Name, GenerateToken(user));

            return new ResponseDto<string>() { Response = "Посилання для підтвердження електронної пошти надіслано на вашу поштову адресу" };
        }

        public async Task<string> ValidateEmail(string email, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return "Електронна пошта не може бути пустою";
            }

            var user = await _repository.Get<User>(e => e.Id != id && e.Email == email);
            if (user != null)
            {
                return "Електронна пошта вже використовується";
            }

            return string.Empty;
        }

        public async Task<string> ValidatePhoneNumber(string phoneNumber, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return "Телефонний номер не може бути пустим";
            }

            var user = await _repository.Get<User>(e => e.Id != id && e.PhoneNumber == phoneNumber);
            if (user != null)
            {
                return "Телефонний номер вже використовується";
            }

            return string.Empty;
        }

        public async Task<string> ValidateCompanyName(string name, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Назва компанії не може бути пустою";
            }

            var user = await _repository.Get<Company>(e => e.Id != id && e.Name == name);
            if (user != null)
            {
                return "Назва компанії вже використовується";
            }

            return string.Empty;
        }

        public async Task<string> ConfirmEmail(int userId)
        {
            var user = await _repository.Get<User>(userId);

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
                new Claim(TokenProperties.FullName, user.Company?.Name ?? $"{user.FirstName} {user.LastName}"),
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

        private async Task<string> ValidateUser(UserRegisterRequestDto dto)
        {
            var error = await ValidateEmail(dto.Email);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            error = await ValidatePhoneNumber(dto.PhoneNumber);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return string.Empty;
        }

        private async Task<string> ValidateCompany(CompanyRegisterRequestDto dto)
        {
            var error = await ValidateEmail(dto.Email);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            error = await ValidateCompanyName(dto.Name);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return string.Empty;
        }
    }
}
