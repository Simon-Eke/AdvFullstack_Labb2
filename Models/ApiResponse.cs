using System.Net;

namespace AdvFullstack_Labb2.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorContent { get; set; }
    }
}
