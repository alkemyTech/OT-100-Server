using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OngProject.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}