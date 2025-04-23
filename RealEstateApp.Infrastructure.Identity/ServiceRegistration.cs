using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Infrastructure.Identity.Contexts;
using RealEstateApp.Infrastructure.Identity.Entities;
using RealEstateApp.Infrastructure.Identity.Seeds;
using RealEstateApp.Infrastructure.Identity.Service;

namespace RealEstateApp.Infrastructure.Identity
{
    public static class ServiceRegistration
    {

        public static void AddIdentityInfrastructureForWebApp(this IServiceCollection services, IConfiguration configuration) 
        {
            #region Context
            ConfigureContext(services, configuration);
            #endregion

            #region Identity
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromSeconds(300);
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;

            })
            .AddCookie(IdentityConstants.ApplicationScheme, opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromHours(24);
                opt.LoginPath = "/User";
                opt.AccessDeniedPath = "/User/AccessDenied";
            });


            #endregion

            #region Services
            services.AddTransient<IWebAppAccountService, AccountServiceForWebApp>();
            #endregion
        }

        private static void ConfigureContext(IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<IdentityContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m=>m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            }); 

        }
        public static async Task RunAsyncSeed(this IServiceProvider serviceProvider) 
        {
            using (var scope = serviceProvider.CreateScope()) 
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var userRepository = services.GetRequiredService<IUserRepository>();
                    var salesTypeRepository = services.GetRequiredService<ISalesTypeRepository>();
                    var propertyTypeRepository = services.GetRequiredService<IPropertyTypeRepository>();
                    var propertyRepository = services.GetRequiredService<IPropertyRepository>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultSalesType.SeedAsync(salesTypeRepository);
                    await DefaultPropertyType.SeedAsync(propertyTypeRepository);


                    await DefaultClient.SeedAsync(userManager, userRepository);
                    await DefaultDeveloper.SeedAsync(userManager, userRepository);
                    await DefaultAdmin.SeedAsync(userManager, userRepository);


                    await DefaultAgents.SeedAsync(userManager,userRepository,propertyRepository);
                    

                }
                catch (Exception ex) 
                { 
                
                }
                
            
            }
        }
    }
}
