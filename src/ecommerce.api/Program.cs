using ecommerce.api.Extensions;
using NG.EF.Common;
using Application = ecommerce.application;

var builder = WebApplication.CreateBuilder(args);
var currentEnviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configurationBuilder = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
             //.AddJsonFile($"appsettings.{currentEnviroment}.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables();

IConfiguration configuration = configurationBuilder.Build();
var efOptions = new EFConfig();
configuration.GetSection(EFConfig.Position).Bind(efOptions);

// Add services to the container.
var applicationAssembly = typeof(Application.AssemblyReference).Assembly;
builder.Services.AddControllers();

builder.Services.AddInfrastructureConfiguration(efOptions);
builder.Services.InjectSQLServerRepositories();
builder.Services.AddHealthchecksConfig();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(applicationAssembly);
});
builder.Services.AddFluentValidation("ecommerce.api.EndpointValidators");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", cfg =>
    {
        cfg.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.SetAppConfiguration();

app.Run();
