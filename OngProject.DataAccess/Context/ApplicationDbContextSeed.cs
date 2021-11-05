using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            // Create roles
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");

            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(userRole);

            context.Roles.Add(new Role
            { Description = userRole.Name, Name = userRole.Name, IdentityId = new Guid(userRole.Id) });
            context.Roles.Add(new Role
            { Description = adminRole.Name, Name = adminRole.Name, IdentityId = new Guid(adminRole.Id) });


            // Create Admin
            var admin = new IdentityUser { UserName = "admin@localhost", Email = "admin@localhost" };

            await userManager.CreateAsync(admin, password: "Abc123.");
            await userManager.AddToRoleAsync(admin, adminRole.Name);
        }

        // Create Organization
        public static async Task SeedDefaultOrganizationAsync(ApplicationDbContext context)
        {
            if (!context.Organizations.Any())
            {
                context.Organizations.Add(new Organization
                {
                    Name = "ONG - Somos Más",
                    Image = "image.png",
                    Address = "None",
                    Phone = 1160112988,
                    Email = "somosfundacionmas@gmail.com",
                    WelcomeText = "bienvenidos",
                    AboutUsText = "Somos una asociación civil sin fines de lucro que se creó en 1997 con la intención de dar alimento a las familias del barrio."
                });
                }
        await context.SaveChangesAsync();

        }



    }
}