using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            
            if(await roleManager.Roles.AllAsync(r => r.Name != adminRole.Name))
                await roleManager.CreateAsync(adminRole);
            
            if(await roleManager.Roles.AllAsync(r => r.Name != userRole.Name))
                await roleManager.CreateAsync(userRole);
            
            
            context.Roles.Add(new Role
                {Description = userRole.Name, Name = userRole.Name, IdentityId = new Guid(userRole.Id)});
            context.Roles.Add(new Role
                {Description = adminRole.Name, Name = adminRole.Name, IdentityId = new Guid(adminRole.Id)});
            
            
            // Create Admin
            var admin = new IdentityUser {UserName = "admin@localhost", Email = "admin@localhost"};

            if (await userManager.Users.AllAsync(u => u.UserName != admin.UserName))
            {
                await userManager.CreateAsync(admin, password: "Abc123.");
                await userManager.AddToRoleAsync(admin, adminRole.Name);
            }
        }
    }
}