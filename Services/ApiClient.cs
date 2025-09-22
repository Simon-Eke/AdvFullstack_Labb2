using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using AdvFullstack_Labb2.Services.IServices;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Helpers;

namespace AdvFullstack_Labb2.Services
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ApiClient> _logger;
        public ApiClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, ILogger<ApiClient> logger)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        private HttpClient CreateClient()
        {
            var client = _clientFactory.CreateClient("MyCafeApi");

            var context = _httpContextAccessor.HttpContext;
            if (context != null && context.Request.Cookies.TryGetValue("JWToken", out var token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<List<TResponse>?> GetAllAsync<TResponse>(string uri)
            where TResponse : class
        {
            try
            {
                var client = CreateClient();
                var response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("API request failed: {StatusCode} for URI: {Uri}", response.StatusCode, uri);
                    throw new HttpRequestException($"API request failed with status {response.StatusCode}: {response.ReasonPhrase}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TResponse>>(json);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError("API request timed out for URI: {Uri}", uri);
                throw new TaskCanceledException("The API request timed out", ex);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request exception for URI: {Uri}", uri);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error for URI: {Uri}", uri);
                throw new JsonException("Failed to deserialize API response", ex);
            }
        }

        public async Task<TResponse?> GetByIdAsync<TResponse>(string uri)
            where TResponse : class
        {
            try
            {
                var client = CreateClient();
                var response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("API request failed: {StatusCode} for URI: {Uri}", response.StatusCode, uri);
                    throw new HttpRequestException($"API request failed with status {response.StatusCode}: {response.ReasonPhrase}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(json);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError("API request timed out for URI: {Uri}", uri);
                throw new TaskCanceledException("The API request timed out", ex);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request exception for URI: {Uri}", uri);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error for URI: {Uri}", uri);
                throw new JsonException("Failed to deserialize API response", ex);
            }
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data)
            where TResponse : class
            where TRequest : class
        {
            try
            {
                var client = CreateClient();
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("API POST request failed: {StatusCode} for URI: {Uri}", response.StatusCode, uri);
                    throw new HttpRequestException($"API request failed with status {response.StatusCode}: {response.ReasonPhrase}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(json);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError("API POST request timed out for URI: {Uri}", uri);
                throw new TaskCanceledException("The API request timed out", ex);
            }
        }
        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data)
            where TResponse : class
            where TRequest : class
        {
            try
            {
                var client = CreateClient();
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await client.PutAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("API PUT request failed: {StatusCode} for URI: {Uri}", response.StatusCode, uri);
                    throw new HttpRequestException($"API request failed with status {response.StatusCode}: {response.ReasonPhrase}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(json);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError("API PUT request timed out for URI: {Uri}", uri);
                throw new TaskCanceledException("The API request timed out", ex);
            }
        }
        public async Task<bool> DeleteAsync(string uri)
        {
            try
            {
                var client = CreateClient();

                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                    return true;

                _logger.LogWarning("API DELETE request failed: {StatusCode} for URI: {Uri}", response.StatusCode, uri);
                return false;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError("API DELETE request timed out for URI: {Uri}", uri);
                throw new TaskCanceledException("The API request timed out", ex);
            }
        }
    }
}
