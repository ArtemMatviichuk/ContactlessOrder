using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper, ICompanyRepository companyRepository)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _companyRepository = companyRepository;
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
                company.ApprovedDate = System.DateTime.Now;
                company.ApprovedById = userId;

                await _adminRepository.SaveChanges();
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
    }
}
