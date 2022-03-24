namespace ContactlessOrder.Common.Dto.Common
{
    public class ResponseDto<T>
    {
        public string ErrorMessage { get; set; }
        public T Response { get; set; }
    }
}
