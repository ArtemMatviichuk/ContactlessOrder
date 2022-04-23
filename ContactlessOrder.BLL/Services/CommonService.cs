using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class CommonService : ICommonService
    {
        private readonly ICateringRepository _cateringRepository;

        public CommonService(ICateringRepository cateringRepository)
        {
            _cateringRepository = cateringRepository;
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
    }
}
