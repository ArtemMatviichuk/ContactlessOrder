namespace ContactlessOrder.DAL.Entities.Companies
{
    public class CateringMenuModification
    {
        public int Id { get; set; }
        public bool Available { get; set; }
        public int? Price { get; set; }
        public bool InheritPrice { get; set; }

        public int CateringId { get; set; }
        public Catering Catering { get; set; }
        public int MenuModificationId { get; set; }
        public MenuModification MenuModification { get; set; }
    }
}
