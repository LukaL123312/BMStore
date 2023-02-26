using BMStore.Api;
using BMStore.Api.Options;
using BMStore.Application;
using BMStore.Infrastructure;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

var watchDogOptions = configuration.GetSection(nameof(WatchDogOptions)).Get<WatchDogOptions>();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();

builder.Services.AddWatchdogLogging(configuration, watchDogOptions);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWatchDogExceptionLogger();

app.UseWatchDog(options => {
    options.WatchPageUsername = watchDogOptions.WatchPageUsername;
    options.WatchPagePassword = watchDogOptions.WatchPagePassword;
});

app.Run();
