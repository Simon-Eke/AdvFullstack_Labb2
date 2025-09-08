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

        public ApiClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
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
            var client = CreateClient();
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TResponse>>(json);
        }
        public async Task<TResponse?> GetByIdAsync<TResponse>(string uri)
            where TResponse : class
        {
            var client = CreateClient();
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(json);
        }
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data)
            where TResponse : class
            where TRequest : class
        {
            var client = CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(json);
        }
        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data)
            where TResponse : class
            where TRequest : class
        {
            var client = CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(json);
        }
        public async Task<bool> DeleteAsync(string uri)
        {
            var client = CreateClient();

            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
