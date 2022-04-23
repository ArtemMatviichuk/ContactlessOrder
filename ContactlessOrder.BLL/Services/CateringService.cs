using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class CateringService : ICateringService
    {
        private readonly ICommonService _commonService;
        private readonly ICateringRepository _cateringRepository;
        private readonly IMapper _mapper;

        public CateringService(ICateringRepository cateringRepository, IMapper mapper, ICommonService commonService)
        {
            _cateringRepository = cateringRepository;
            _mapper = mapper;
            _commonService = commonService;
        }

        public async Task<IEnumerable<CateringMenuOptionDto>> GetMenu(int cateringId)
        {
            var menu = await _cateringRepository.GetMenu(cateringId);

            var dtos = _mapper.Map<IEnumerable<CateringMenuOptionDto>>(menu);

            return dtos.OrderBy(e => e.Name);
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

        public async Task<IEnumerable<CateringModificationDto>> GetModifications(int cateringId)
        {
            return await _commonService.GetCateringModifications(cateringId);
        }

        public async Task UpdateModification(int id, int cateringId, UpdateCateringMenuOptionDto dto)
        {
            var modification = await _cateringRepository.Get<CateringModification>(e => e.CateringId == cateringId && e.ModificationId == id);

            if (modification != null)
            {
                modification.Available = dto.Available;
                modification.InheritPrice = dto.InheritPrice;
                modification.Price = dto.InheritPrice ? null : dto.Price;
            }
            else
            {
                modification = new CateringModification()
                {
                    Price = dto.Price,
                    Available = dto.Available,
                    InheritPrice = dto.InheritPrice,
                    CateringId = cateringId,
                    ModificationId = id,
                };

                await _cateringRepository.Add(modification);
            }

            await _cateringRepository.SaveChanges();
        }
    }
}
