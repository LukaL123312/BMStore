using BMStore.Application.Behaviors;
using BMStore.Application.Commands;
using BMStore.Application.Handlers.CommandHandlers;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace BMStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        return services;
    }

    public static IServiceCollection AddModelValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddUserCommand>, AddUserCommandValidator>();

        services.AddTransient<IRequestHandler<AuthenticateCommand, AuthenticateCommandResponse>, AuthenticateCommandHandler>();

        return services;
    }
}

