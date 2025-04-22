
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using System.Reflection;


namespace RealEstateApp.Core.Application
{
    public static class ServiceRegistration
    {

       
        public static void AddApplicationLayerForWebApp(this IServiceCollection service) 
        {
            GenericService(service);
            GenericConfigurations(service);
        }

        #region Generic
        private static void GenericService(this IServiceCollection service)
        {
            service.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IClientService, ClientService>();
            service.AddTransient<IAgentService, AgentService>();
            service.AddTransient<IPropertyService, PropertyService>();


        }

        private static void GenericConfigurations(this IServiceCollection service) 
        { 
            service.AddAutoMapper(Assembly.GetExecutingAssembly()); 
        }
        #endregion

    }
}
