using AdvFullstack_Labb2.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IApiClient _client;
        public BaseController(IApiClient client, IHttpContextAccessor httpContextAccessor) 
        {
            _client = client;

            var token = httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _client.SetToken(token);
            }
        }
        protected bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken"));
        }
    }
}
