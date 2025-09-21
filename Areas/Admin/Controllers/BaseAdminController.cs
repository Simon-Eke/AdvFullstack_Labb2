using AdvFullstack_Labb2.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdvFullstack_Labb2.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        protected readonly IApiClient _client;
        public BaseAdminController(IApiClient client) 
        {
            _client = client;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var hasToken = HttpContext.Request.Cookies.ContainsKey("JWToken");

            if (!hasToken)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
