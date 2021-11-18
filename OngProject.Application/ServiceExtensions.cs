using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OngProject.Application.Helpers.Mail;
using OngProject.Application.Interfaces.Mail;
using OngProject.Application.Services;
using OngProject.Application.Services.Mail;
using SendGrid.Extensions.DependencyInjection;

namespace OngProject.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<MemberService>();
            services.AddScoped<ActivityService>();
            services.AddScoped<NewsService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<OrganizationService>();
            services.AddScoped<UserDetailsService>();
            services.AddScoped<TestimonyService>();
            services.AddScoped<SlideService>();
            services.AddScoped<ContactService>();

            #region MailService
            services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));
            services.AddSendGrid(options =>
            {
                options.ApiKey = configuration["MailConfiguration:SendGridApiKey"];
            });
            services.AddScoped<IMailService, MailService>();
            #endregion

            services.AddScoped<CommentService>();

            return services;
        }
    }
}