namespace ContactlessOrder.Common.Dto.Caterings
{
    public class CateringMenuOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int Price { get; set; }
        public bool InheritPrice { get; set; }

        public int MenuOptionId { get; set; }
    }
}
