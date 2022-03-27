using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Users;
using ContactlessOrder.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IUserRepository _repository;

        public ValidationService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ValidateUser(UserRegisterRequestDto dto, int? id = null)
        {
            var error = await ValidateEmail(dto.Email, id);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            error = await ValidatePhoneNumber(dto.PhoneNumber, id);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return string.Empty;
        }

        public async Task<string> ValidateCompany(CompanyRegisterRequestDto dto, int? id = null)
        {
            var error = await ValidateEmail(dto.Email, id);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            error = await ValidateCompanyName(dto.Name, id);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return string.Empty;
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

        public async Task<string> ValidateCompanyName(string name, int? userId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Назва компанії не може бути пустою";
            }

            var user = await _repository.Get<Company>(e => e.UserId != userId && e.Name == name);
            if (user != null)
            {
                return "Назва компанії вже використовується";
            }

            return string.Empty;
        }
    }
}
