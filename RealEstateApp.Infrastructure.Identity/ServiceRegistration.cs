using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Settings;
using RealEstateApp.Infrastructure.Identity.Contexts;
using RealEstateApp.Infrastructure.Identity.Entities;
using RealEstateApp.Infrastructure.Identity.Seeds;
using RealEstateApp.Infrastructure.Identity.Service;
using System.Text;

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

        public static void AddIdentityInfrastructureForWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            ConfigureContext(services, configuration);
            #endregion

            #region Identity
            services.AddIdentityCore<ApplicationUser>()
               .AddRoles<IdentityRole>()
               .AddSignInManager()
               .AddEntityFrameworkStores<IdentityContext>()
               .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("REFRESHTOKENPROVIDER");

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromSeconds(300);
            });

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not Authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not Authorized to access this resource" });
                        return c.Response.WriteAsync(result);
                    }
                };
            });
           

            #endregion

            #region Services
            services.AddTransient<IWebApiAccountService, AccountServiceForWebApi>();
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
