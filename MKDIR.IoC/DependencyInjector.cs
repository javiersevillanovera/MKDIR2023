using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MKDIR.Domain;
using MKDIR.Infrastructure;
using MKDIR.Service;
using MKDIR.Service.AccessControl.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.IoC
{
    public class DependencyInjector
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            RegisterRepositories(services, configuration);
            RegisterServices(services, configuration);
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBusinessUserRepository, BusinessUserRepository>();
            services.AddScoped<IBusinessModuleRepository, BusinessModuleRepository>();
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IJwtAuthManager, JwtAuthManagerService>();
            services.AddScoped<IBusinessUserService, BusinessUserService>();
            services.AddScoped<IBusinessModuleService, BusinessModuleService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtAuthManagerService, JwtAuthManagerService>();
        }
    }
}
