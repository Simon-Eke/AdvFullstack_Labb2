using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace AdvFullstack_Labb2.Services
{
    public class ApiAuthClient : IApiAuthClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ApiAuthClient> _logger;

        public ApiAuthClient(IHttpClientFactory clientFactory, ILogger<ApiAuthClient> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        // Make an auth apiclient if more endpoints than loginasync
        public async Task<string?> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var client = _clientFactory.CreateClient("MyCafeApi");
                var json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(ApiRoutes.Auth.Base, content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Login failed with status: {StatusCode} for user: {Username}",
                        response.StatusCode, loginRequest.Username);

                    // Throw with specific status code info so ApiExceptionFilter can handle it
                    throw new HttpRequestException($"Login failed with status {response.StatusCode}: {response.ReasonPhrase}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                return loginResponse?.Jwt;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError("Login request timed out for user: {Username}", loginRequest.Username);
                throw new TaskCanceledException("The login request timed out", ex);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request exception during login for user: {Username}", loginRequest.Username);
                throw; // Re-throw to let ApiExceptionFilter handle it
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error during login for user: {Username}", loginRequest.Username);
                throw new JsonException("Failed to process login response", ex);
            }
        }
    }
}
