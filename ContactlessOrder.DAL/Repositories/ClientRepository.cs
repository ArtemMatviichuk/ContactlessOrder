using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Repositories
{
    public class ClientRepository : RepositoryBase, IClientRepository
    {
        public ClientRepository(ContactlessOrderContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Catering>> GetCateringsByCoordinates(CoordinateDto from, CoordinateDto to)
        {
            return await Context.Set<Catering>()
                .Include(e => e.Coordinates)
                .Include(e => e.Company)
                .Where(e => e.Coordinates.Lat >= from.Lat && e.Coordinates.Lat <= to.Lat
                    && e.Coordinates.Lng >= from.Lng && e.Coordinates.Lng <= to.Lng)
                .ToListAsync();
        }
    }
}
