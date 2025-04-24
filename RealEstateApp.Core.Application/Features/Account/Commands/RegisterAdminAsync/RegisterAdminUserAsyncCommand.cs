

using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.Features.Account.Commands.RegisterDevelopAsync
{
    /// <summary>
    /// Propiedades necesarias para registrar un desarrollador
    /// </summary>
    public class RegisterAdminUserAsyncCommand : IRequest<int>
    {
     
        public string UserName {  get; set; }
        
        public string FirstName { get;set; }
   
        public string LastName { get; set; }
      
        public string Email {  get; set; }
    
        public string IdentityCard { get; set; }

        public string Password { get; set; }


    }

    public class RegisterAdminUserAsyncHandler : IRequestHandler<RegisterAdminUserAsyncCommand, int>
    {
        private readonly IWebApiAccountService _webApiAccountService;
        private IMapper _mapper;

        public RegisterAdminUserAsyncHandler(IWebApiAccountService webApiAccountService, IMapper mapper ) 
        {
            _webApiAccountService= webApiAccountService;
            _mapper = mapper;
        }


        public async Task<int> Handle(RegisterAdminUserAsyncCommand request, CancellationToken cancellationToken)
        {
            var user=await _webApiAccountService.RegisterAdminUserAsync(_mapper.Map<RegisterRequest>(request));
            return 1;
        }
    }
}
