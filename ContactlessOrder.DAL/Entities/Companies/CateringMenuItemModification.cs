namespace ContactlessOrder.DAL.Entities.Companies
{
    public class CateringModification
    {
        public int Id { get; set; }
        public int? Price { get; set; }
        public bool Available { get; set; }
        public bool InheritPrice { get; set; }

        public int CateringId { get; set; }
        public Catering Catering { get; set; }
        public int ModificationId { get; set; }
        public Modification Modification { get; set; }
    }
}
