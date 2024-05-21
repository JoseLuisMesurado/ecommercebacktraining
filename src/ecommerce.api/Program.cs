using MediatR;
using NG.API.Extensions.Behaviors;
using NG.EF.Common;
using Serilog;
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SerilogPipelineBehavior<,>));

builder.Services.AddInfrastructureConfiguration(efOptions);
builder.Services.InjectSQLServerRepositories();
builder.Services.AddHealthchecksConfig();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(applicationAssembly);
    //config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.ProblemDetailsConfiguration();
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
