using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace AdvFullstack_Labb2.Filters
{
    // Big Note!

    // The idea is mine since I had a generic apiclient I could create wrappers and filters around it
    // for exception handling, authorization/role based access, logging, caching if needed



    // but the whole code is taken from Claude AI given my apiclient, apiauthclient and account and adminscontroller as context.
    
    
    // This decision was made for centralized exception handling and lack of time 


    // Simon Eke, 2025-09-21

    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Exception occurred in controller: {Controller}, Action: {Action}",
                context.RouteData.Values["controller"], context.RouteData.Values["action"]);

            if (context.Exception is HttpRequestException httpEx)
            {
                // Handle HTTP exceptions from your ApiClient calls
                HandleHttpRequestException(context, httpEx);
            }
            else if (context.Exception is TaskCanceledException)
            {
                // Handle timeout exceptions
                HandleTimeoutException(context);
            }
            else if (context.Exception is JsonException ||
                     context.Exception.Message.Contains("JSON") ||
                     context.Exception.Message.Contains("deserialization"))
            {
                // Handle JSON deserialization errors
                HandleJsonException(context);
            }
            else if (context.Exception is InvalidOperationException invalidOpEx &&
                    invalidOpEx.Message.Contains("The view") &&
                    invalidOpEx.Message.Contains("was not found"))
            {
                _logger.LogWarning(invalidOpEx, "Missing view encountered");

                SetTempDataAndRedirect(context,
                    "The page you're trying to access does not exist or the view is missing.",
                    "ViewMissing");

                return;
            }
        }

        private void HandleHttpRequestException(ExceptionContext context, HttpRequestException httpEx)
        {
            // Extract status code from the exception message if possible
            var message = httpEx.Message.ToLower();

            if (message.Contains("401") || message.Contains("unauthorized"))
            {
                context.HttpContext.Response.Cookies.Delete("JWToken");
                _logger.LogWarning("Unauthorized access attempt, redirecting to login");
                // JWT token might be expired or invalid
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
            else if (message.Contains("404") || message.Contains("not found"))
            {
                // Resource not found - redirect back with error
                SetTempDataAndRedirect(context, "The requested resource was not found.");
            }
            else if (message.Contains("400") || message.Contains("bad request"))
            {
                // Bad request - likely validation error
                SetTempDataAndRedirect(context, "Invalid request. Please check your input.");
            }
            else if (message.Contains("500") || message.Contains("internal server"))
            {
                // Server error
                SetTempDataAndRedirect(context, "Server error occurred. Please try again later.");
            }
            else
            {
                // Generic HTTP error
                SetTempDataAndRedirect(context, "Service temporarily unavailable. Please try again.", "ApiDown");
            }

            context.ExceptionHandled = true;
        }

        private void HandleTimeoutException(ExceptionContext context)
        {
            _logger.LogWarning("Request timed out for controller: {Controller}, Action: {Action}",
            context.RouteData.Values["controller"], context.RouteData.Values["action"]);

            SetTempDataAndRedirect(context, "The service is taking too long to respond. Please try again.", "Timeout");
            context.ExceptionHandled = true;
        }

        private void HandleJsonException(ExceptionContext context)
        {
            _logger.LogError("JSON deserialization error in controller: {Controller}, Action: {Action}",
            context.RouteData.Values["controller"], context.RouteData.Values["action"]);

            SetTempDataAndRedirect(context, "Invalid response from server. Please try again.", "JsonError");
            context.ExceptionHandled = true;
        }

        private void SetTempDataAndRedirect(ExceptionContext context, string errorMessage, string errorType = "General")
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var area = context.RouteData.Values["area"]?.ToString();

            // Try to set TempData through service provider

            try
            {
                var tempDataFactory = context.HttpContext.RequestServices.GetRequiredService<ITempDataDictionaryFactory>();
                var tempData = tempDataFactory.GetTempData(context.HttpContext);
                tempData["Error"] = errorMessage;
                tempData["ErrorType"] = errorType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't set TempData in ApiExceptionFilter");
            }
            

            // Determine where to redirect based on area
            if (!string.IsNullOrEmpty(area) && area.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // quick fix
                context.Result = new RedirectToActionResult("ErrorApiDown", "Home", new { area = "" });
                // Redirect to admin area index
                // context.Result = new RedirectToActionResult("Index", controller ?? "Admins", new { area = "Admin" });
            }
            else
            {
                // Redirect to main area
                context.Result = new RedirectToActionResult("ErrorApiDown", "Home", new { area = "" });
            }

            context.ExceptionHandled = true;
        }
    }
}
