using System.Collections.Generic;

namespace ContactlessOrder.DAL.Entities.Companies
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<MenuItemPicture> Pictures { get; set; }
        public ICollection<MenuItemOption> Options { get; set; }
        public ICollection<MenuItemModification> MenuItemModifications { get; set; }
    }
}
