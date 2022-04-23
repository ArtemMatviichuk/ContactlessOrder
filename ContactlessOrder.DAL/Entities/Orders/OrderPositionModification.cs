using ContactlessOrder.DAL.Entities.Companies;

namespace ContactlessOrder.DAL.Entities.Orders
{
    public class OrderPositionModification
    {
        public int Id { get; set; }
        public int InMomentPrice { get; set; }
        public string ModificationName { get; set; }

        public int OrderPositionId { get; set; }
        public OrderPosition OrderPosition { get; set; }
        public int? ModificationId { get; set; }
        public Modification Modification { get; set; }
    }
}
