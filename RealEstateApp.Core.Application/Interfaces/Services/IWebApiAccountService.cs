

using RealEstateApp.Core.Application.Dtos.Account;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IWebApiAccountService : IAccountService
    {
        Task<AuthenticationResponseForApi> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request, string origin);
    }
}
