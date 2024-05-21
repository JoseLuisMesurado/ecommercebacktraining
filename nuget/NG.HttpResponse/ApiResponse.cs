namespace NG.HttpResponse
{
    public class ApiResponse
    {
        public IList<string> Messages { get; set; } = new List<string> { "Success" };
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Response { get; set; }
    }
}
