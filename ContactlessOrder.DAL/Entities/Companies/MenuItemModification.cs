namespace ContactlessOrder.DAL.Entities.Companies
{
    public class MenuItemModification
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public int ModificationId { get; set; }
        public Modification Modification { get; set; }
    }
}
