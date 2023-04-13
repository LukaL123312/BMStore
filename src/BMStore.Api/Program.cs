using BMStore.Api;
using BMStore.Api.Middlewares;
using BMStore.Api.Options;

using BMStore.Application;

using BMStore.Infrastructure;
using BMStore.Infrastructure.Extensions;

using Serilog;

using WatchDog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSwagger();

var configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();

builder.Services.AddModelValidation();

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var watchDogOptions = configuration.GetSection(nameof(WatchDogOptions)).Get<WatchDogOptions>();
builder.Services.AddWatchdogLogging(configuration, watchDogOptions);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

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

app.UseSerilogRequestLogging();
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