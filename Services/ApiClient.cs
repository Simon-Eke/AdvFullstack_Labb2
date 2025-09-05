using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using AdvFullstack_Labb2.Services.IServices;

namespace AdvFullstack_Labb2.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _client;
        private string? _token;
        // private static readonly JsonSerializerOptions...

        public ApiClient(HttpClient client)
        {
            _client = client;
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        private void AddAuthHeader()
        {
            if (!string.IsNullOrEmpty(_token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }
            else
            {
                _client.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task<List<TResponse>?> GetAllAsync<TResponse>(string uri)
            where TResponse : class
        {
            AddAuthHeader();

            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TResponse>>(json);
        }
        public async Task<TResponse?> GetByIdAsync<TResponse>(string uri)
            where TResponse : class
        {
            AddAuthHeader();

            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(json);
        }
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data)
            where TResponse : class
            where TRequest : class
        {
            AddAuthHeader();

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseBody);
        }
        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data)
            where TResponse : class
            where TRequest : class
        {
            AddAuthHeader();

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseBody);
        }
        public async Task<bool> DeleteAsync(string uri)
        {
            AddAuthHeader();

            var response = await _client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }
    }
}
