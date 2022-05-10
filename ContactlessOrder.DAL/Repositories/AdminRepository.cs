using ContactlessOrder.Common.Constants;
using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Users;
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
                .Where(e => e.User.EmailConfirmed && (!approved && !e.ApprovedDate.HasValue) || (approved && e.ApprovedDate.HasValue))
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Context.Set<User>()
                .Include(e => e.Company)
                .Include(e => e.Role)
                .Where(e => e.EmailConfirmed && e.Company == null && e.Role.Value == UserRoles.ClientValue)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSupport()
        {
            return await Context.Set<User>()
                .Include(e => e.Company)
                .Include(e => e.Role)
                .Where(e => e.Company == null && e.Role.Value == UserRoles.SupportValue)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAdministrators()
        {
            return await Context.Set<User>()
                .Include(e => e.Company)
                .Include(e => e.Role)
                .Where(e => e.Company == null && e.Role.Value == UserRoles.AdminValue)
                .ToListAsync();
        }
    }
}
