using BookLib.Models.DBModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLib.Initializers
{
    public class UsersInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminName = "admin";
            string adminPassword = "123456";
            string userName = "user";
            string userPassword = "123456";
            string user1Name = "user1";
            string user1Password = "123456";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminName) == null)
            {
                ApplicationUser admin = new ApplicationUser { UserName = adminName };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            if (await userManager.FindByNameAsync(userName) == null)
            {
                ApplicationUser user = new ApplicationUser { UserName = userName };
                IdentityResult result = await userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
            if (await userManager.FindByNameAsync(user1Name) == null)
            {
                ApplicationUser user = new ApplicationUser { UserName = user1Name };
                IdentityResult result = await userManager.CreateAsync(user, user1Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }
    }
}
