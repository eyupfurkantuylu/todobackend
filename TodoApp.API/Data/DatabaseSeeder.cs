using Microsoft.AspNetCore.Identity;
using TodoApp.API.Models;
using TodoApp.API.Models.Identity;

namespace TodoApp.API.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            // Rolleri oluştur
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Demo admin kullanıcısı oluştur
            var adminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            if (await _userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                var result = await _userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Demo normal kullanıcı oluştur
            var demoUser = new ApplicationUser
            {
                UserName = "user@example.com",
                Email = "user@example.com",
                EmailConfirmed = true
            };

            if (await _userManager.FindByEmailAsync(demoUser.Email) == null)
            {
                var result = await _userManager.CreateAsync(demoUser, "User123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(demoUser, "User");
                }
            }
        }
    }
} 