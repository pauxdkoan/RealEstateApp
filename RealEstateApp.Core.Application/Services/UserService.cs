

using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agent;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.ViewModels.User.Admin;
using RealEstateApp.Core.Application.ViewModels.User.Developer;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : GenericService<SaveUserVm, UserVm, User, string>, IUserService
    {
        private readonly IWebAppAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IWebAppAccountService accountService, IMapper mapper, IUserRepository userRepository)
        :base(repository:userRepository,mapper:mapper)

        {
            _accountService = accountService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<RegisterResponse> UpdateUserAsync(SaveUserVm vm) 
        {
            var updateRequest= _mapper.Map<UpdateRequest>(vm);
            switch (vm.Rol) 
            {
                case (int) Roles.Administrador:
                    updateRequest.Rol=Roles.Administrador.ToString();
                    break;
                case (int)Roles.Agente:
                    updateRequest.Rol = Roles.Agente.ToString();
                    break;
                case (int)Roles.Desarrollador:
                    updateRequest.Rol = Roles.Desarrollador.ToString();
                    break;
                case (int)Roles.Cliente:
                    updateRequest.Rol = Roles.Cliente.ToString();
                    break;
            }

            var response= await _accountService.UpdateUser(updateRequest);
            return response;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginVm vm) 
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse= await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }

        public async Task SignOutAsync() 
        { 
            await _accountService.SignOutAsync();
        }

        public async Task<List<string>?> GetAllRoles() 
        { 
            var rolesList= await _accountService.GetAllRoles();
            rolesList=rolesList.Where(s=>s.Contains(Roles.Cliente.ToString()) || s.Contains(Roles.Agente.ToString())).ToList();
            return rolesList;
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserVm vm, string origin) 
        { 
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);

            switch (vm.Rol) 
            {
                case (int)Roles.Cliente:
                    registerRequest.Rol=Roles.Cliente.ToString();
                    break;

                case (int)Roles.Agente:
                    registerRequest.Rol = Roles.Agente.ToString();
                    break;

                case (int)Roles.Desarrollador:
                    registerRequest.Rol = Roles.Desarrollador.ToString();
                    break;

                case (int)Roles.Administrador:
                    registerRequest.Rol = Roles.Administrador.ToString();
                    break;
            }
            
            
            return await _accountService.RegisterUserAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token) 
        { 
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task UpdateStatusAsync(string idUser) 
        { 
            await _accountService.UpdateStatusAsync(idUser, null);
        }

        //Solo obtener desarrolladores
        public async Task<List<DeveloperVm>> GetAllDevelopers()
        {
            List<DeveloperVm> devs = new List<DeveloperVm>();
            List<UserVm> developersVm = _mapper.Map<List<UserVm>>(await _userRepository.GetAllListAsync());
            devs = _mapper.Map<List<DeveloperVm>>(developersVm.Where(s => s.Rol == Roles.Desarrollador.ToString()).ToList());
            return devs;
        }

        public async Task<List<AgentViewModel>> GetAllAgents()
        {
            List<AgentViewModel> agents = new List<AgentViewModel>();
            List<UserVm> agentsVm = _mapper.Map<List<UserVm>>(await _userRepository.GetAllListAsync());
            agents = _mapper.Map<List<AgentViewModel>>(agentsVm.Where(s => s.Rol == Roles.Agente.ToString()).ToList());
            return agents;
        }

        public async Task<UserVm> GetByIdViewModel(string id)
        {
            UserVm user = _mapper.Map<UserVm>(await _userRepository.GetByIdAsync(id));
            return user;
        }
    }
}
