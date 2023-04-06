using BMStore.Api.Options;

using WatchDog;

namespace BMStore.Api;

public static class DependencyInjection
{
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