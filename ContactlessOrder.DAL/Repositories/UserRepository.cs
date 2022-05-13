using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.Users;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(ContactlessOrderContext context)
            : base(context)
        {
        }

        public async Task<User> GetUser(int id)
        {
            return await Context.Set<User>()
                .Include(e => e.Company)
                .Include(e => e.Catering)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetUser(string email)
        {
            return await Context.Set<User>()
                .Include(e => e.Company)
                .Include(e => e.Catering)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Email == email
                    || (e.Email != null &&  e.Email.StartsWith($"{email}@")));
        }
    }
}
