using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MKDIR.Domain;
using MKDIR.WebApp;
using MKDIR.WebApp.Authentication;
using MKDIR.WebApp.Interfaces.Repository;
using MKDIR.WebApp.Interfaces.Services;
using MKDIR.WebApp.Repository;
using MKDIR.WebApp.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var backendAPIurl = builder.Configuration.GetValue<string>("BackendAPI");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(backendAPIurl) });
ConfigureServices(builder.Services);
await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IWebApiRepository, WebApiRepository>();
    services.AddAuthorizationCore();

    services.AddScoped<ProveedorAutenticacionJWT>();
    services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>(proveedor =>
        proveedor.GetRequiredService<ProveedorAutenticacionJWT>());

    services.AddScoped<ILoginServiceJwT, ProveedorAutenticacionJWT>(proveedor =>
        proveedor.GetRequiredService<ProveedorAutenticacionJWT>());

    services.AddScoped<ILoginService, LoginService>();

    services.AddScoped<RenovadorToken>();
}
