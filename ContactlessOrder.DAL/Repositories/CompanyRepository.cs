using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Repositories
{
    public class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        public CompanyRepository(ContactlessOrderContext context) : base(context)
        {
        }

        public async Task<Catering> GetCatering(int id)
        {
            return await Context.Set<Catering>().Include(e => e.Coordinates).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Catering>> GetCaterings(int userId)
        {
            return await Context.Set<Catering>()
                .Include(e => e.Company)
                .Include(e => e.Coordinates)
                .Include(e => e.MenuOptions)
                .Where(e => e.Company.UserId == userId)
                .ToListAsync();
        }

        public async Task<Company> GetCompany(int userId)
        {
            return await Context.Set<Company>()
                .Include(e => e.User)
                .Include(e => e.PaymentData)
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItems(int userId)
        {
            return await Context.Set<MenuItem>()
                .Include(e => e.Options)
                .Include(e => e.MenuItemModifications)
                .Include(e => e.Company)
                .Include(e => e.Pictures)
                .Where(e => e.Company.UserId == userId)
                .ToListAsync();
        }
    }
}
