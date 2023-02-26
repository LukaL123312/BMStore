using BMStore.Application.Interfaces.IUnitOfWork;
using BMStore.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BMStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<BMStoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BMStoreDbConnectionString")));
        var buildServiceProvider = services.BuildServiceProvider();
        if ((buildServiceProvider.GetService(typeof(BMStoreDbContext)) is BMStoreDbContext libraryAppDbContext))
        {
            libraryAppDbContext.Database.EnsureCreated();
            libraryAppDbContext.Dispose();
        }

        return services;
    }
}
