using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ContactlessOrder.BLL.Infrastructure;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Users;
using ContactlessOrder.DAL.Entities.User;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ContactlessOrder.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _secret;
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IOptions<AppSettings> appSettings, IUserRepository repository, IConfiguration configuration)
        {
            _secret = configuration["AppSettings:Secret"];
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<ResponseDto<string>> Authenticate(UserLoginRequestDto dto)
        {
            var passwordHash = CryptoHelper.GetMd5Hash(dto.Password);
            var user = await _repository.Get<User>(e => e.Email == dto.Email);

            if (user == null)
            {
                return new ResponseDto<string>() { ErrorMessage = "User not found" };
            }
            else if (user.Password != passwordHash)
            {
                return new ResponseDto<string>() { ErrorMessage = "Password is incorrect" };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var claims = new List<Claim>
            {
                new Claim(TokenProperties.Id, user.Id.ToString()),
                new Claim(TokenProperties.Email, user.Email),
                new Claim(TokenProperties.FullName, $"{user.FirstName} {user.LastName}"),
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                     SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescription));
            return new ResponseDto<string>() { Response = token };
        }

        public Task<ResponseDto<string>> Register(UserRegisterRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
