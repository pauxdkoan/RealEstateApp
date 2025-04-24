

using RealEstateApp.Core.Application.Dtos.Account;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IWebApiAccountService : IAccountService
    {
        Task<AuthenticationResponseForApi> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request);
    }
}
