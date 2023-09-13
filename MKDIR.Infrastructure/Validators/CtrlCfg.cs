using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Infrastructure
{
    public static class CtrlCfg
    {
        public static IServiceCollection AddControllersExtend(this IServiceCollection services)
        {
            //services.AddControllers(options =>
            //{
            //    options.Filters.Add<GlobalValidationFilterAttribute>();
            //}).ConfigureApiBehaviorOptions(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //}).AddFluentValidation(conf =>
            //{
            //    conf.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //});

            //services.AddControllers().AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            //    options.UseCamelCasing(false);
            //});

            //services.AddControllers().AddFluentValidation(conf =>
            //{
            //    conf.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //});

            //services.AddControllers(options =>
            //{
            //    options.Filters.Add<GlobalValidationFilterAttribute>();
            //}).ConfigureApiBehaviorOptions(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //}).AddFluentValidation(conf =>
            //{
            //    conf.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //});


            return services;
        }
    }
}
