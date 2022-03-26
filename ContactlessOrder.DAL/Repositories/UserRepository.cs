using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Interfaces;

namespace ContactlessOrder.DAL.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(ContactlessOrderContext context)
            : base(context)
        {
        }
    }
}
