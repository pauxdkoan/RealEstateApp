

using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
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
            registerRequest.Rol=vm.Rol==1?Roles.Cliente.ToString():Roles.Agente.ToString();
            return await _accountService.RegisterUserAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token) 
        { 
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

       
    }
}
