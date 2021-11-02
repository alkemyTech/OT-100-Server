using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OngProject.Application.Interfaces;
using OngProject.DataAccess.Context;

namespace OngProject.DataAccess
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(ops =>
                ops.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), m =>
                    m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}