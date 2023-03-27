using BMStore.Application.Interfaces.IRepositories;
using BMStore.Application.Interfaces.IUnitOfWork;
using BMStore.Infrastructure.Data.DbContext;
using BMStore.Infrastructure.Data.Repository;
using BMStore.Infrastructure.Identity.Models;
using BMStore.Infrastructure.Identity.Models.Authentication;
using BMStore.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BMStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager<SignInManager<ApplicationUser>>();

        services.AddScoped<ITokenService, TokenService>();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

        services.AddAuthentication(options =>
        {
            //options.AddGoogle
            //       .AddFacebook
        });

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BmStoreDbConnectionString")));

        services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            // Configure password requirements
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 1;
        });

        // HttpContext
        services.AddHttpContextAccessor();

        // Strongly-typed configurations using IOptions
        services.Configure<Token>(configuration.GetSection("Token"));

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(1);
        });

        services.AddDbContext<BMStoreDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BMStoreDbConnectionString")));
        var buildServiceProvider = services.BuildServiceProvider();
        if ((buildServiceProvider.GetService(typeof(BMStoreDbContext)) is BMStoreDbContext bmStoreDbContext))
        {
            bmStoreDbContext.Database.EnsureCreated();
            bmStoreDbContext.Dispose();
        }

        return services;
    }
}