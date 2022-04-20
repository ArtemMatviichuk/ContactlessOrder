namespace ContactlessOrder.DAL.Entities.Companies
{
    public class MenuModification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
