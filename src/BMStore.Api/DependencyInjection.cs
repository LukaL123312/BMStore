using BMStore.Api.Options;
using BMStore.Application.Commands;
using BMStore.Application.Queries;
using FluentValidation;
using WatchDog;

namespace BMStore.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddModelValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddUserCommand>, AddUserCommandValidator>();

        return services;
    }

    public static IServiceCollection AddWatchdogLogging(this IServiceCollection services, IConfiguration configuration, WatchDogOptions watchDogOptions)
    {
        if (watchDogOptions == null)
            throw new ArgumentNullException(nameof(watchDogOptions));

        services.AddSingleton(watchDogOptions);

        services.AddWatchDogServices(options =>
        {
            options.IsAutoClear = false;
            options.SetExternalDbConnString = configuration.GetConnectionString("BMStoreDbConnectionString");
            options.SqlDriverOption = WatchDog.src.Enums.WatchDogSqlDriverEnum.MSSQL;
        });

        return services;
    }
}