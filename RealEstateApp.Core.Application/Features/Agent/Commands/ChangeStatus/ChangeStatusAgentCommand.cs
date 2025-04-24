
using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.Features.Agent.Commands.ChangeStatus
{
    /// <summary>
    /// Parámetros para cambiar el estado de un agente
    /// </summary>
    public class ChangeStatusAgentCommand : IRequest<bool>
    {
        [SwaggerParameter(Description ="El id del agente que se quiere cambiar su estado")]

        public string Id { get; set; }

        [SwaggerParameter(Description = "Debe indicar el estado el cual tendra el agente Activo(true)/Inactivo(false)")]
        public bool Status {  get; set; }
    }

    public class ChangeStatusAgentCommandHandler : IRequestHandler<ChangeStatusAgentCommand, bool>
    {
        private readonly IWebApiAccountService _accountService;
        public ChangeStatusAgentCommandHandler(IWebApiAccountService webAppAccountService) 
        {
            _accountService = webAppAccountService;
        }
        public async Task<bool> Handle(ChangeStatusAgentCommand request, CancellationToken cancellationToken)
        {
            await _accountService.UpdateStatusAsync(request.Id, request.Status);
            return true;
        }

       
    }
}
