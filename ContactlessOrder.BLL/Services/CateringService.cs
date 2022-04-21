using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Orders;
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
        private readonly IMapper _mapper;

        public CateringService(ICateringRepository cateringRepository, IMapper mapper)
        {
            _cateringRepository = cateringRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CateringMenuOptionDto>> GetMenu(int cateringId)
        {
            var menu = await _cateringRepository.GetMenu(cateringId);

            var dtos = _mapper.Map<IEnumerable<CateringMenuOptionDto>>(menu);

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
