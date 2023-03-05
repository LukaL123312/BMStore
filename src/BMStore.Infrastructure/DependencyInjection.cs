using BMStore.Application.Interfaces.IRepositories;
using BMStore.Application.Interfaces.IUnitOfWork;
using BMStore.Infrastructure.Data.DbContext;
using BMStore.Infrastructure.Data.Repository;
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

        services.AddDbContext<BMStoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BMStoreDbConnectionString")));
        var buildServiceProvider = services.BuildServiceProvider();
        if ((buildServiceProvider.GetService(typeof(BMStoreDbContext)) is BMStoreDbContext bmStoreDbContext))
        {
            bmStoreDbContext.Database.EnsureCreated();
            bmStoreDbContext.Dispose();
        }

        return services;
    }
}