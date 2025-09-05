using Microsoft.AspNetCore.Http;

namespace AdvFullstack_Labb2.Services.IServices
{
    public interface IApiClient
    {
        void SetToken(string token);
        Task<List<TResponse>?> GetAllAsync<TResponse>(string uri)
            where TResponse : class;
        Task<TResponse?> GetByIdAsync<TResponse>(string uri)
            where TResponse : class;
        Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data)
            where TResponse : class
            where TRequest : class;
        Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data)
            where TResponse : class
            where TRequest : class;
        Task<bool> DeleteAsync(string uri);
    }
}
