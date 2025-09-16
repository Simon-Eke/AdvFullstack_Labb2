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

        public ApiAuthClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // Make an auth apiclient if more endpoints than loginasync
        public async Task<string?> LoginAsync(LoginRequest loginRequest)
        {
            var client = _clientFactory.CreateClient("MyCafeApi");

            var json = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ApiRoutes.Auth.Base, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

            return loginResponse.Jwt;
        }
    }
}
