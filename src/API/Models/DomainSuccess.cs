namespace API.Models
{
    public class DomainSuccess<T>
    {
        public T ResponseValue { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
