


using RealEstateApp.Core.Application.Dtos.Account;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<List<string>> GetAllRoles();
        Task<RegisterResponse> UpdateUser(UpdateRequest request);

        Task UpdateStatusAsync(string idUser);

    }
}
