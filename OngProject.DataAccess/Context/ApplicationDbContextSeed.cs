using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OngProject.DataAccess.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Create roles
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");

            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(userRole);
            
            // Create Admin
            var admin = new IdentityUser {UserName = "admin@localhost", Email = "admin@localhost"};

            await userManager.CreateAsync(admin, password: "Abc123.");
            await userManager.AddToRoleAsync(admin, adminRole.Name);
        }
    }
}