namespace ContactlessOrder.DAL.Entities.Companies
{
    public class Coordinate
    {
        public int Id { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }

        public Catering Catering { get; set; }
    }
}
