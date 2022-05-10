using AutoMapper;
using ContactlessOrder.BLL.Infrastructure;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Users;
using ContactlessOrder.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository; 
        private readonly IUserRepository _userRepository; 
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper,
            ICompanyRepository companyRepository, INotificationService notificationService, IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies(bool approved)
        {
            var companies = await _adminRepository.GetCompanies(approved);
            var dtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return dtos;
        }
        public async Task ApproveCompany(int id, int userId)
        {
            var company = await _adminRepository.Get<Company>(id);

            if (company != null)
            {
                company.ApprovedDate = DateTime.Now;
                company.ApprovedById = userId;

                await _adminRepository.SaveChanges();

                await _notificationService.NotifyCompanyUpdated(company.UserId);
            }
        }

        public async Task RejectCompany(int id)
        {
            var company = await _adminRepository.Get<Company>(id);

            if (company != null)
            {
                company.ApprovedDate = null;
                company.ApprovedById = null;

                await _adminRepository.SaveChanges();

                await _notificationService.NotifyCompanyUpdated(company.UserId);
            }
        }

        public async Task<PaymentDataDto> GetCompanyPaymentData(int id)
        {
            var data = await _adminRepository.Get<PaymentData>(id);

            return data == null ? null : _mapper.Map<PaymentDataDto>(data);
        }

        public async Task<IEnumerable<CateringDto>> GetCaterings(int id)
        {
            var company = await _adminRepository.Get<Company>(id);

            if (company != null)
            {
                var catering = await _companyRepository.GetCaterings(company.UserId);

                return _mapper.Map<IEnumerable<CateringDto>>(catering);
            }

            return Array.Empty<CateringDto>();
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _adminRepository.GetUsers();
            var dtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return dtos;
        }

        public async Task BlockUser(int id)
        {
            var user = await _adminRepository.Get<User>(id);
            
            if (user != null)
            {
                user.IsBlocked = !user.IsBlocked;
                await _adminRepository.SaveChanges();

                await _notificationService.NotifyUserUpdated(id);
            }
        }

        public async Task<IEnumerable<UserDto>> GetSupport()
        {
            var users = await _adminRepository.GetSupport();
            var dtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return dtos;
        }

        public async Task<IEnumerable<UserDto>> GetAdministrators()
        {
            var users = await _adminRepository.GetAdministrators();
            var dtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return dtos;
        }

        public async Task<string> DeleteUser(int id, int userId)
        {
            if (userId == id)
            {
                return "Ви не можете видалити свій аккаунт";
            }

            var user = await _userRepository.GetUser(id);

            if (user != null)
            {
                await _adminRepository.Remove<User>(id);
                await _adminRepository.SaveChanges();

                await _notificationService.NotifyUserDeleted(id);

                return string.Empty;
            }

            return "Користувач не знайдений";
        }

        public async Task<ResponseDto<string>> RegenerateUserPassword(int id)
        {
            var user = await _userRepository.GetUser(id);

            if (user != null)
            {
                var password = CryptoHelper.GeneratePassword(16);
                user.PasswordHash = CryptoHelper.GetMd5Hash(password);

                await _adminRepository.SaveChanges();

                return new ResponseDto<string> { Response = password };
            }

            return new ResponseDto<string> { ErrorMessage = "Користувач не знайдений" };
        }

        public async Task<UserLoginRequestDto> CreateUser(CreateUserDto dto)
        {
            var role = await _adminRepository.Get<Role>(e => e.Value == dto.RoleValue);

            if (role != null)
            {
                var login = CryptoHelper.GeneratePassword(16);
                var password = CryptoHelper.GeneratePassword(16);
                var user = new User()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PhoneNumber = dto.PhoneNumber,
                    EmailConfirmed = true,
                    Email = login,
                    PasswordHash = CryptoHelper.GetMd5Hash(password),
                    RoleId = role.Id,
                    RegistrationDate = DateTime.UtcNow,
                };

                await _userRepository.Add(user);
                await _userRepository.SaveChanges();

                await _notificationService.NotifyUserRegistered(user.Id);

                return new UserLoginRequestDto()
                {
                    Email = login,
                    Password = password,
                };
            }

            return null;
        }
    }
}
