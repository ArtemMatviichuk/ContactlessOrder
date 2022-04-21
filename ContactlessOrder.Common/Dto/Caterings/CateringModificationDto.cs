namespace ContactlessOrder.Common.Dto.Caterings
{
    public class CateringModificationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
        public bool InheritPrice { get; set; }
    }
}
