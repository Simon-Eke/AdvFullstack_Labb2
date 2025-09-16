using AdvFullstack_Labb2.Models;

namespace AdvFullstack_Labb2.Services.IServices
{
    public interface IApiAuthClient
    {
        Task<string?> LoginAsync(LoginRequest loginRequest);
    }
}
