using ContactlessOrder.Common.Dto.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface ISupportService
    {
        Task<IEnumerable<ComplainDto>> GetComplains(int statusValue);
        Task ChangeComplainStatus(int id, int status, int userId);
    }
}
