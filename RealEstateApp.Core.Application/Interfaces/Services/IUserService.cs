

using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Agent;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.ViewModels.User.Developer;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserVm, UserVm, User, string>
    {
        Task<AuthenticationResponse> LoginAsync(LoginVm vm);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterAsync(SaveUserVm vm, string origin);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<List<string>> GetAllRoles();
        Task<RegisterResponse> UpdateUserAsync(SaveUserVm vm);
        Task UpdateStatusAsync(string idUser);
        Task<List<DeveloperVm>> GetAllDevelopers();
        Task<List<AgentViewModel>> GetAllAgents();
        Task<UserVm> GetByIdViewModel(string id);
    }
}
