using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Dtos.Agent
{
    internal class AgentChangeStatus
    {
        [SwaggerParameter(Description = "Indica si el agente esta activo (true) o inactivo (false)")]
        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }
    }
}
