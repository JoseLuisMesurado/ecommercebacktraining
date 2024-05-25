using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiExtension
    {
        public static IServiceCollection AddHealthchecksConfig(this IServiceCollection services)
        {
            services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }

        public static IApplicationBuilder AddHealthChecksConfig(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/ready", new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains("dependencies"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }

        
        public static IApplicationBuilder SetAppConfiguration(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            // if (app.Environment.IsDevelopment())
            // {
            app.UseSwagger();
            app.UseSwaggerUI();
            //}
            //app.UseSerilogRequestLogging();
            app.UseCors("AllowAll");
            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.AddHealthChecksConfig();
            return app;
        }
    }
}
