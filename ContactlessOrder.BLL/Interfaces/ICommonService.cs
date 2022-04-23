using ContactlessOrder.Common.Dto.Caterings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface ICommonService
    {
        Task<IEnumerable<CateringModificationDto>> GetCateringModifications(int cateringId);
    }
}
