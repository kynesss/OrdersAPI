using Microsoft.AspNetCore.Identity;
using OrdersAPI.Constants;
using OrdersAPI.Entities;

namespace OrdersAPI.Seeders
{
    public class IdentitySeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Roles.User));
            }

            if (!_userManager.Users.Any())
            {
                var admin = new User
                {
                    UserName = "admin@orders.com",
                    Email = "admin@orders.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    DateOfBirth = new DateTime(1990, 1, 1)
                };

                var user = new User
                {
                    UserName = "user@orders.com",
                    Email = "user@orders.com",
                    FirstName = "Kamil",
                    LastName = "Pirog",
                    DateOfBirth = new DateTime(2020, 5, 10)
                };

                await _userManager.CreateAsync(admin, "Kamil123!");
                await _userManager.CreateAsync(user, "Kamil123!");

                await _userManager.AddToRoleAsync(admin, Roles.Admin);
                await _userManager.AddToRoleAsync(user, Roles.User);
            }
        }
    }
}