using BMStore.Api;
using BMStore.Api.Middlewares;
using BMStore.Api.Options;

using BMStore.Application;

using BMStore.Infrastructure;
using BMStore.Infrastructure.Extensions;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSwagger();

var configuration = builder.Configuration;

var watchDogOptions = configuration.GetSection(nameof(WatchDogOptions)).Get<WatchDogOptions>();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();

builder.Services.AddModelValidation();

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddWatchdogLogging(configuration, watchDogOptions);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();

    // Optional: auto-create and seed Identity DB
    app.EnsureIdentityDbIsCreated();
    app.SeedIdentityDataAsync().Wait();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseWatchDogExceptionLogger();

app.UseWatchDog(options =>
{
    options.WatchPageUsername = watchDogOptions.WatchPageUsername;
    options.WatchPagePassword = watchDogOptions.WatchPagePassword;
});

app.Run();