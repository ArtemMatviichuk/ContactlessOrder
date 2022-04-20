using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class CateringService : ICateringService
    {
        private readonly ICateringRepository _cateringRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CateringService(ICateringRepository cateringRepository, IMapper mapper, ICompanyRepository companyRepository)
        {
            _cateringRepository = cateringRepository;
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<CateringMenuOptionDto>> GetMenu(int cateringId)
        {
            var menu = await _cateringRepository.GetMenu(cateringId);

            var dtos = _mapper.Map<IEnumerable<CateringMenuOptionDto>>(menu);

            return dtos;
        }

        public async Task<IEnumerable<MenuItemDto>> GetModifications(int cateringId)
        {
            var catering = await _companyRepository.GetCatering(cateringId);
            var menuItems = await _cateringRepository.GetMenuItems(catering.CompanyId);

            var neededItems = menuItems.Where(e =>
                catering.MenuOptions.Select(o => o.MenuOption.MenuItemId).Contains(e.Id));

            var modificationIds = neededItems.SelectMany(i => i.Modifications.Select(m => m.Id));
            var cateringMenuModifications = await _cateringRepository.GetAll<CateringMenuModification>(e =>
                e.CateringId == cateringId && modificationIds.Contains(e.MenuModificationId));

            var dtos = _mapper.Map<IEnumerable<MenuItemDto>>(neededItems);

            foreach (var item in dtos.SelectMany(e => e.Modifications))
            {
                var cMod = cateringMenuModifications.FirstOrDefault(e => e.MenuModificationId == item.Id);
                if (cMod != null)
                {
                    item.Price = cMod.InheritPrice ? item.Price : cMod.Price.Value;
                    item.Available = cMod.Available;
                }
                else
                {
                    item.Available = true;
                }
            }

            return dtos;
        }

        public async Task<string> UpdateMenuOption(int id, UpdateCateringMenuOptionDto dto)
        {
            var option = await _cateringRepository.GetMenuOption(id);

            if (option != null)
            {
                option.Available = dto.Available;
                option.InheritPrice = dto.InheritPrice;
                option.Price = dto.InheritPrice ? null : dto.Price;

                await _cateringRepository.SaveChanges();
                return string.Empty;
            }

            return "Опція не знайдена";
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(int cateringId)
        {
            var orders = await _cateringRepository.GetOrders(cateringId);

            var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return dtos;
        }
    }
}
