﻿using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly IConfiguration _configuration;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper,
            FileHelper fileHelper, IConfiguration configuration, IValidationService validationService)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _configuration = configuration;
            _validationService = validationService;
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

                var password = CryptoHelper.GeneratePassword(16);
                catering.Login = $"{company.Name}.{catering.Name}".ToLower().Replace(" ", "_");
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
            var catering = await _companyRepository.Get<Catering>(id);

            if (catering != null)
            {
                _mapper.Map(dto, catering);

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
    }
}