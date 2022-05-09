using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Repositories
{
    public class AdminRepository : RepositoryBase, IAdminRepository
    {
        public AdminRepository(ContactlessOrderContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Company>> GetCompanies(bool approved)
        {
            return await Context.Set<Company>()
                .Include(e => e.User)
                .Include(e => e.PaymentData)
                .Where(e => (!approved && !e.ApprovedDate.HasValue) || (approved && e.ApprovedDate.HasValue))
                .ToListAsync();
        }
    }
}
