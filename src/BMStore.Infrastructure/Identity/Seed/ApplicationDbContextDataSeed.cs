using BMStore.Domain.Constants;
using BMStore.Infrastructure.Identity.Models;

using Microsoft.AspNetCore.Identity;

namespace BMStore.Infrastructure.Identity.Seed;

public static class ApplicationDbContextDataSeed
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Add roles supported
        await roleManager.CreateAsync(new IdentityRole(ApplicationIdentityConstants.Roles.Administrator));
        await roleManager.CreateAsync(new IdentityRole(ApplicationIdentityConstants.Roles.Member));

        // New admin user
        string adminUserName = "admin";
        string adminEmail = "admin@gmail.com";
        var adminUser = new ApplicationUser
        {
            UserName = adminUserName,
            PhoneNumber = "",
            TwoFactorEnabled = false,
            PhoneNumberConfirmed = true,
            LockoutEnabled = false,
            AuthenticatorKey = "",
            AccessFailedCount = 0,
            Email = adminEmail,
            IsEnabled = true,
            IsDeleted = false,
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "Administrator"
        };
        var user = await userManager.FindByEmailAsync(adminEmail);
        await userManager.DeleteAsync(user);

        // Add new user and their role

        await userManager.CreateAsync(adminUser, ApplicationIdentityConstants.DefaultPassword);
        await userManager.AddToRoleAsync(adminUser, ApplicationIdentityConstants.Roles.Administrator);
    }
}
