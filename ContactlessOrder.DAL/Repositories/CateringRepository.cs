using ContactlessOrder.Common.Constants;
using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Orders;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Repositories
{
    public class CateringRepository : RepositoryBase, ICateringRepository
    {
        public CateringRepository(ContactlessOrderContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<CateringMenuOption>> GetMenu(int cateringId)
        {
            return await Context.Set<CateringMenuOption>()
                .Include(e => e.MenuOption.MenuItem.Pictures)
                .Include(e => e.MenuOption.MenuItem.Options)
                .Where(e => e.CateringId == cateringId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItemPicture>> GetMenuItemPictures(int id)
        {
            return await Context.Set<MenuItemPicture>()
                .Include(p => p.MenuItem.Options)
                .Where(p => p.MenuItem.Options.Select(e => e.Id).Contains(id))
                .ToListAsync();
        }

        public async Task<CateringMenuOption> GetMenuOption(int id)
        {
            return await Context.Set<CateringMenuOption>()
                .Include(e => e.MenuOption.MenuItem)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<CateringMenuOption>> GetMenuOptions(int cateringId, IEnumerable<int> ids)
        {
            return await Context.Set<CateringMenuOption>()
                .Include(e => e.MenuOption.MenuItem.Pictures)
                .Include(e => e.Catering.Company)
                .Where(e => e.CateringId == cateringId && ids.Contains(e.MenuOptionId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders(int cateringId)
        {
            return await Context.Set<Order>()
                .Include(e => e.Status)
                .Include(e => e.Positions)
                .ThenInclude(e => e.Option.MenuOption.MenuItem.Pictures)
                .Where(e => e.Positions.Select(e => e.Option.CateringId).FirstOrDefault() == cateringId
                    && e.Status.Value > OrderStatuses.CreatedStatusValue)
                .ToListAsync();
        }

        public async Task<Catering> GetFullCatering(int cateringId)
        {
            return await Context.Set<Catering>()
                .Include(e => e.CateringModifications)
                .Include(e => e.MenuOptions)
                .ThenInclude(e => e.MenuOption.MenuItem.MenuItemModifications)
                .ThenInclude(e => e.Modification)
                .FirstOrDefaultAsync(e => e.Id == cateringId);
        }
    }
}
