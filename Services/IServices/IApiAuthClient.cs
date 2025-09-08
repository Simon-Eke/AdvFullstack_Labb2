namespace AdvFullstack_Labb2.Services.IServices
{
    public interface IApiAuthClient
    {
        Task<string?> LoginAsync(string username, string password);
    }
}
