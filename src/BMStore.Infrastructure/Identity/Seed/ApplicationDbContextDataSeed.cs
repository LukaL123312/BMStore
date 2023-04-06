using BMStore.Domain.Constants;
using BMStore.Infrastructure.Identity.Models;

using Microsoft.AspNetCore.Identity;

namespace BMStore.Infrastructure.Identity.Seed;

public class ApplicationDbContextDataSeed
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
            NormalizedUserName = adminUserName,
            NormalizedEmail = adminEmail,
            UserName = adminUserName,
            Email = adminEmail,
            IsEnabled = true,
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "Administrator"
        };
        //var user = await  userManager.FindByEmailAsync(adminEmail);

        //await userManager.DeleteAsync(user);


        var validator = new UserValidator<ApplicationUser>();
        var validationResults = await validator.ValidateAsync(userManager, adminUser);

        // Add new user and their role
        var abc = await userManager.CreateAsync(adminUser, ApplicationIdentityConstants.DefaultPassword);
        //adminUser = await userManager.FindByNameAsync(adminUserName);
        await userManager.AddToRoleAsync(adminUser, ApplicationIdentityConstants.Roles.Administrator);
    }
}
