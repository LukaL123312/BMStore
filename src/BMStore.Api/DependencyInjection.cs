using BMStore.Api.Options;

using Microsoft.OpenApi.Models;

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

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                var securityRequirement = new OpenApiSecurityRequirement
                        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        new string[] { }
                    }
                        };

                options.AddSecurityDefinition("bearerAuth", securityScheme);
                options.AddSecurityRequirement(securityRequirement);
            });

        return services;
    }
}