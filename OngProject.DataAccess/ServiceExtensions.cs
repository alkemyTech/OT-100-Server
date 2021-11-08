using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.Identity;
using OngProject.DataAccess.Context;
using OngProject.DataAccess.Identity;

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

            #region Authentication

            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

            services.AddAuthentication(ops =>
                {
                    ops.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    ops.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    ops.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        RoleClaimType = ClaimTypes.Role,
                        ValidateLifetime = true
                    };
                });

            services.AddDefaultIdentity<ApplicationUser>(ops => ops.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenHandlerService, TokenHandlerService>();

            #endregion

            return services;
        }
    }
}