


using AutoMapper;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.ViewModels.User.Admin;

namespace RealEstateApp.Core.Application.Services
{
    public class AdminService: IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public AdminService(IUserRepository userRepository, IPropertyRepository propertyRepository
            , IMapper mapper
            )
        {
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _mapper = mapper;

        }

        public async Task<AdminVm> GetDataForDashBoard() 
        { 
            var userList=  await _userRepository.GetAllListAsync();

            var propertyList = await _propertyRepository.GetAllListAsync();
            var clientList= userList.Where(u=>u.Rol== Roles.Cliente.ToString()).ToList();
            var agentList = userList.Where(u => u.Rol == Roles.Agente.ToString()).ToList();
            var developerList = userList.Where(u => u.Rol == Roles.Desarrollador.ToString()).ToList();

            return new AdminVm
            {
                Agents = _mapper.Map<List<UserVm>>(agentList),
                Clients = _mapper.Map<List<UserVm>>(clientList),
                Developers = _mapper.Map<List<UserVm>>(developerList),
                Properties = _mapper.Map<List<PropertyVm>>(propertyList),
            };


            

        }


    }
}
