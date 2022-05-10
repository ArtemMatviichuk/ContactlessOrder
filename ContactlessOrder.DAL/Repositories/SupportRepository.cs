using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.Users;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Repositories
{
    public class SupportRepository : RepositoryBase, ISupportRepository
    {
        public SupportRepository(ContactlessOrderContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Complain>> GetComplains(int statusValue)
        {
            return await Context.Set<Complain>()
                .Include(e => e.User)
                .Include(e => e.Catering)
                .ThenInclude(e => e.Company.User)
                .Include(e => e.Order)
                .Where(e => (int)e.Status == statusValue)
                .ToListAsync();
        }

        public async Task<Complain> GetComplain(int id)
        {
            return await Context.Set<Complain>()
                .Include(e => e.User)
                .Include(e => e.Catering)
                .ThenInclude(e => e.Company.User)
                .Include(e => e.Order)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetUser(int id)
        {
            return await Context.Set<User>()
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
