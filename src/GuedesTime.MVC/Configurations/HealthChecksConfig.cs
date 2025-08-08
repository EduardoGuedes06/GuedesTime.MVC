using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace GuedesTime.MVC.Configurations
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection AddHealthChecksConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("connection");

            services.AddHealthChecks()
                .AddMySql(
                    connectionString,
                    name: "Banco de Dados",
                    healthQuery: "SELECT 1;",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "database" }
                )
                .AddCheck("Sistema", () =>
                    HealthCheckResult.Healthy("Sistema está funcionando perfeitamente!"),
                    tags: new[] { "system" }
                );

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            return services;
        }

        public static IApplicationBuilder UseHealthChecksConfig(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthChecksResponseWriter.WriteResponse
            });

            app.UseHealthChecksUI(config =>
            {
                config.UIPath = "/monitoramento";
            });

            return app;
        }

        public static class HealthChecksResponseWriter
        {
            public static async Task WriteResponse(HttpContext context, HealthReport report)
            {
                context.Response.ContentType = "application/json";
                var result = new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        description = entry.Value.Description,
                        duration = entry.Value.Duration
                    })
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
