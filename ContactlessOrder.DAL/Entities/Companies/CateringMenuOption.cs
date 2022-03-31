namespace ContactlessOrder.DAL.Entities.Companies
{
    public class CateringMenuOption
    {
        public int Id { get; set; }
        public bool Available { get; set; }
        public int? Price { get; set; }
        public bool InheritPrice { get; set; }

        public int CateringId { get; set; }
        public Catering Catering { get; set; }
        public int MenuOptionId { get; set; }
        public MenuItemOption MenuOption { get; set; }
    }
}
