namespace ISP_API.Dtos
{
    public class ResponseDto<T>
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}