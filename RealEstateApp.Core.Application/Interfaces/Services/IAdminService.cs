

using RealEstateApp.Core.Application.ViewModels.User.Admin;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAdminService
    {
        Task<AdminVm> GetDataForDashBoard();
    }
}
