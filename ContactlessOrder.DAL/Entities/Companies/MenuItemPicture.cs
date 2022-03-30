using System;

namespace ContactlessOrder.DAL.Entities.Companies
{
    public class MenuItemPicture
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
