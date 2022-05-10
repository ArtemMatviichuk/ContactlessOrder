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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IValidationService _validationService;
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly IConfiguration _configuration;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper,
            FileHelper fileHelper, IConfiguration configuration, IValidationService validationService, INotificationService notificationService)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _configuration = configuration;
            _validationService = validationService;
            _notificationService = notificationService;
        }

        public async Task<CompanyDto> GetCompany(int userId)
        {
            var company = await _companyRepository.GetCompany(userId);

            var dto = _mapper.Map<CompanyDto>(company);

            return dto;
        }

        public async Task<FileDto> GetCompanyLogo(int userId)
        {
            var user = await _companyRepository.Get<User>(userId);
            if (user == null || string.IsNullOrEmpty(user.ProfilePhotoPath))
            {
                return null;
            }

            return await _fileHelper.GetFile(user.ProfilePhotoPath, _configuration[AppConstants.FilePath]);
        }

        public async Task<string> UpdateCompanyData(int userId, UpdateCompanyDataDto dto)
        {
            var user = await _companyRepository.Get<User>(userId);
            var company = await _companyRepository.Get<Company>(e => e.UserId == userId);

            if (user == null || company == null)
            {
                return "Компанія не знайдена";
            }

            var error = await _validationService.ValidateCompany(dto, userId);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            user.ModifiedDate = DateTime.Now;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;

            company.Name = dto.Name;
            company.Address = dto.Address;
            company.Description = dto.Description;

            if (dto.Logo != null)
            {
                user.ProfilePhotoPath =
                    await _fileHelper.SaveFile(dto.Logo, _configuration[AppConstants.FilePath]);
            }
            else if (dto.RemoveLogo)
            {
                File.Delete(Path.Combine(_configuration[AppConstants.FilePath], user.ProfilePhotoPath));
                user.ProfilePhotoPath = null;
            }

            await _companyRepository.SaveChanges();

            await _notificationService.NotifyCompanyUpdated(userId);

            return string.Empty;
        }

        public async Task<IEnumerable<CateringDto>> GetCaterings(int userId)
        {
            var caterings = await _companyRepository.GetCaterings(userId);

            var dtos = _mapper.Map<IEnumerable<CateringDto>>(caterings);

            return dtos;
        }

        public async Task<UserLoginRequestDto> CreateCatering(int userId, CreateCateringDto dto)
        {
            var company = await _companyRepository.GetCompany(userId);

            if (company != null)
            {
                var catering = _mapper.Map<Catering>(dto);
                catering.CompanyId = company.Id;

                if (dto.MenuIds != null && dto.MenuIds.Any())
                {
                    catering.MenuOptions = dto.MenuIds.Select(e => new CateringMenuOption() { MenuOptionId = e, Available = true, InheritPrice = true }).ToList();
                }

                var password = CryptoHelper.GeneratePassword(16);
                catering.Login = CryptoHelper.GeneratePassword(16);
                catering.PasswordHash = CryptoHelper.GetMd5Hash(password);

                await _companyRepository.Add(catering);
                await _companyRepository.SaveChanges();

                return new UserLoginRequestDto()
                {
                    Email = catering.Login,
                    Password = password,
                };
            }

            return null;
        }

        public async Task UpdateCatering(int id, CreateCateringDto dto)
        {
            var catering = await _companyRepository.GetCatering(id);

            if (catering != null)
            {
                _mapper.Map(dto, catering);

                await RemoveCateringMenuOptions(id, dto.MenuIds);

                await _companyRepository.SaveChanges();
            }
        }

        public async Task DeleteCatering(int id)
        {
            await _companyRepository.Remove<Catering>(id);
            await _companyRepository.SaveChanges();
        }

        public async Task<string> RegenerateCateringPassword(int id)
        {
            var catering = await _companyRepository.Get<Catering>(id);

            if (catering != null)
            {
                var password = CryptoHelper.GeneratePassword(16);
                catering.PasswordHash = CryptoHelper.GetMd5Hash(password);

                await _companyRepository.SaveChanges();

                return password;
            }

            return string.Empty;
        }

        public async Task<IEnumerable<MenuItemDto>> GetMenu(int userId)
        {
            var menu = await _companyRepository.GetMenuItems(userId);

            var dtos = _mapper.Map<IEnumerable<MenuItemDto>>(menu);

            return dtos;
        }

        public async Task<IEnumerable<IdNameDto>> GetMenuOptions(int userId)
        {
            var menu = await _companyRepository.GetMenuItems(userId);

            var dtos = menu.SelectMany(e => e.Options).Select(e => new IdNameDto() { Id = e.Id, Name = $"{e.MenuItem.Name} ({e.Name})" });

            return dtos;
        }

        public async Task CreateMenuItem(int userId, CreateMenuItemDto dto)
        {
            var company = await _companyRepository.GetCompany(userId);

            if (company != null)
            {
                var menuItem = _mapper.Map<MenuItem>(dto);
                menuItem.CompanyId = company.Id;

                if (dto.Pictures != null && dto.Pictures.Any())
                {
                    menuItem.Pictures = await SavePictures(dto.Pictures);
                }

                await _companyRepository.Add(menuItem);
                await _companyRepository.SaveChanges();
            }
        }

        public async Task UpdateMenuItem(int id, UpdateMenuItemDto dto)
        {
            var menuItem = await _companyRepository.Get<MenuItem>(id);

            if (menuItem != null)
            {
                _mapper.Map(dto, menuItem);

                if (dto.Pictures != null && dto.Pictures.Any())
                {
                    menuItem.Pictures = await SavePictures(dto.Pictures);
                }

                if (dto.DeletedPictureIds != null && dto.DeletedPictureIds.Any())
                {
                    var pictures = await _companyRepository.GetAllAsTracking<MenuItemPicture>(p =>
                            dto.DeletedPictureIds.Contains(p.Id));

                    _companyRepository.RemoveRange(pictures);
                }

                await RemoveOptions(id, dto.Options);
                await RemoveModifications(id, dto.Modifications);

                await _companyRepository.SaveChanges();
            }
        }

        public async Task DeleteMenuItem(int id)
        {
            await _companyRepository.Remove<MenuItem>(id);
            await _companyRepository.SaveChanges();
        }

        public async Task<IEnumerable<AttachmentDto>> GetMenuItemPictures(int id)
        {
            var pictures = await _companyRepository.GetAll<MenuItemPicture>(p => p.MenuItemId == id);

            var dtos = _mapper.Map<IEnumerable<AttachmentDto>>(pictures);

            return dtos;
        }

        public async Task<FileDto> GetMenuItemPictureFile(int id)
        {
            var attachment = await _companyRepository.Get<MenuItemPicture>(id);
            if (attachment == null)
            {
                return null;
            }

            return await _fileHelper.GetFile(attachment.FileName, _configuration[AppConstants.FilePath]);
        }

        public async Task<IEnumerable<IdNamePriceDto>> GetModifications(int userId)
        {
            var company = await _companyRepository.Get<Company>(e => e.UserId == userId);
            var modifications = await _companyRepository.GetAll<Modification>(e => e.CompanyId == company.Id);

            var dtos = _mapper.Map<IEnumerable<IdNamePriceDto>>(modifications);

            return dtos;
        }

        public async Task CreateModification(int userId, ValueDto<IEnumerable<NamePriceDto>> dto)
        {
            var company = await _companyRepository.Get<Company>(e => e.UserId == userId);
            var modifications = _mapper.Map<IEnumerable<Modification>>(dto.Value);

            foreach (var item in modifications)
            {
                item.CompanyId = company.Id;
            }

            await _companyRepository.AddRange(modifications);
            await _companyRepository.SaveChanges();
        }

        public async Task UpdateModification(int id, NamePriceDto dto)
        {
            var modification = await _companyRepository.Get<Modification>(id);
            _mapper.Map(dto, modification);
            await _companyRepository.SaveChanges();
        }

        public async Task DeleteModification(int id)
        {
            await _companyRepository.Remove<Modification>(id);
            await _companyRepository.SaveChanges();
        }

        public async Task<PaymentDataDto> GetCompanyPaymentData(int userId)
        {
            var company = await _companyRepository.GetCompany(userId);

            if (company != null)
            {
                var data = await _companyRepository.Get<PaymentData>(e => e.CompanyId == company.Id);

                return data == null ? null : _mapper.Map<PaymentDataDto>(data);
            }

            return null;
        }

        public async Task UpdateCompanyPaymentData(int userId, PaymentDataDto dto)
        {
            var company = await _companyRepository.GetCompany(userId);

            if (company != null)
            {
                var data = await _companyRepository.Get<PaymentData>(e => e.CompanyId == company.Id);

                if (data != null)
                {
                    _mapper.Map(dto, data);
                }
                else
                {
                    data = _mapper.Map<PaymentData>(dto);
                    data.CompanyId = company.Id;

                    await _companyRepository.Add(data);
                }

                company.ApprovedDate = null;
                company.ApprovedById = null;

                await _companyRepository.SaveChanges();
            }
        }

        private async Task<List<MenuItemPicture>> SavePictures(IEnumerable<IFormFile> files)
        {
            var pictures = new List<MenuItemPicture>();
            foreach (var picture in files)
            {
                var fileName = await _fileHelper.SaveFile(picture, _configuration[AppConstants.FilePath]);

                pictures.Add(new MenuItemPicture()
                {
                    FileName = fileName,
                    CreatedDate = DateTime.UtcNow,
                });
            }

            return pictures;
        }

        private async Task RemoveOptions(int menuId, IEnumerable<IdNamePriceDto> dtos)
        {
            if (dtos != null && dtos.Any())
            {
                var toDelete = await _companyRepository.GetAllAsTracking<MenuItemOption>(e => e.MenuItemId == menuId && !dtos.Select(o => o.Id).Contains(e.Id));

                _companyRepository.RemoveRange(toDelete);
            }
            else
            {
                var options = await _companyRepository.GetAll<MenuItemOption>(e => e.MenuItemId == menuId);
                _companyRepository.RemoveRange(options);
            }
        }

        private async Task RemoveModifications(int menuId, IEnumerable<int> ids)
        {
            var modifications = await _companyRepository.GetAll<MenuItemModification>(e => e.MenuItemId == menuId);

            if (ids != null && ids.Any())
            {
                var toDelete = modifications.Where(e => !ids.Contains(e.ModificationId));
                var toCreate = ids.Where(e => !modifications.Select(e => e.ModificationId).Contains(e))
                    .Select(e => new MenuItemModification() { MenuItemId = menuId, ModificationId = e });

                _companyRepository.RemoveRange(toDelete);
                await _companyRepository.AddRange(toCreate);
            }
            else
            {
                _companyRepository.RemoveRange(modifications);
            }
        }

        private async Task RemoveCateringMenuOptions(int cateringId, IEnumerable<int> ids)
        {
            var options = await _companyRepository.GetAll<CateringMenuOption>(e => e.CateringId == cateringId);

            if (ids != null && ids.Any())
            {
                var toDelete = options.Where(e => !ids.Contains(e.MenuOptionId));
                var toCreate = ids.Where(e => !options.Select(e => e.MenuOptionId).Contains(e))
                    .Select(e => new CateringMenuOption() { CateringId = cateringId, MenuOptionId = e, Available = true, InheritPrice = true });

                _companyRepository.RemoveRange(toDelete);
                await _companyRepository.AddRange(toCreate);
            }
            else
            {
                _companyRepository.RemoveRange(options);
            }
        }
    }
}
