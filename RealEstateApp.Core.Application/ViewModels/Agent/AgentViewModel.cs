using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Agent
{
    internal class AgentViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
