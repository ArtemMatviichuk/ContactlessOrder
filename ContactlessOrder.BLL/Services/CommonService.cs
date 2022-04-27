using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class CommonService : ICommonService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICateringRepository _cateringRepository;

        public CommonService(ICateringRepository cateringRepository, IClientRepository clientRepository)
        {
            _cateringRepository = cateringRepository;
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<CateringModificationDto>> GetCateringModifications(int cateringId)
        {
            var catering = await _cateringRepository.GetFullCatering(cateringId);

            var cateringModifications = catering.CateringModifications;
            var modifications = catering.MenuOptions.SelectMany(e =>
                    e.MenuOption.MenuItem.MenuItemModifications.Select(m => m.Modification))
                .GroupBy(e => e.Id)
                .Select(e => e.First());

            return modifications.Select(m =>
            {
                var item = cateringModifications.FirstOrDefault(e => e.ModificationId == m.Id);
                return new CateringModificationDto()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Price = item == null || item.InheritPrice ? m.Price : item.Price.Value,
                    Available = item == null ? true : item.Available,
                    InheritPrice = item == null ? true : item.InheritPrice,
                };
            });
        }

        public async Task<int> GetOrderTotalPrice(int id, int userId)
        {
            var order = await _clientRepository.GetOrder(id);

            if (order != null && (order.UserId == userId || userId == AppConstants.ViewAll))
            {
                if (order.Status.Value != OrderStatuses.CreatedStatusValue)
                {
                    return order.Positions.Select(e =>
                    (e.InMomentPrice + e.Modifications.Select(m => m.InMomentPrice).Sum())
                        * e.Quantity).Sum();
                }

                var cateringId = order.Positions.First().Option.CateringId;
                var modifications = await GetCateringModifications(cateringId);

                return order.Positions.Select(e =>
                ((e.Option.InheritPrice
                    ? e.Option.MenuOption.Price
                    : e.Option.Price.Value) +
                  e.Modifications.Select(m =>
                    modifications.First(cm => cm.Id == m.ModificationId)
                        .Price).Sum()) * e.Quantity).Sum();
            }

            return -1;
        }
    }
}
