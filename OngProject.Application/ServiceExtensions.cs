using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OngProject.Application.Services;

namespace OngProject.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(MemberService));
            services.AddScoped(typeof(TestimonyService));
            services.AddScoped(typeof(RoleService));
            
            return services;
        }

    }
}