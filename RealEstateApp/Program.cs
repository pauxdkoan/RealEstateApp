using RealEstateApp.Core.Application;
using RealEstateApp.Infrastructure.Identity;
using RealEstateApp.Infrastructure.Persistence;
using RealEstateApp.Infrastructure.Shared;
using RealEstateApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
//Layers y Contexts:
builder.Services.AddApplicationLayerForWebApp();//Application
builder.Services.AddSharedInfrastructure(builder.Configuration);//Shared
builder.Services.AddIdentityInfrastructureForWebApp(builder.Configuration);//Identity
builder.Services.AddPersistenceInfrastructure(builder.Configuration);//Context

//Other
builder.Services.AddScoped<LoginAuthorize>(); //Para el middleware
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();//Para el middleraware
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Seed
await app.Services.RunAsyncSeed();
app.UseSession();//Session
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
