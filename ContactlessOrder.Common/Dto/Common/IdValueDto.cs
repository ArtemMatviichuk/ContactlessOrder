namespace ContactlessOrder.Common.Dto.Common
{
    public class IdValueDto<T>
    {
        public int Id { get; set; }
        public T Value { get; set; }
    }
}
