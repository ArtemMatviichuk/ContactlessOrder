using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.DAL.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface IClientRepository : IRepositoryBase
    {
        Task<IEnumerable<Catering>> GetCateringsByCoordinates(CoordinateDto from, CoordinateDto to);
    }
}
