using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Timeouts;
using ticket_reservation_platform.Helpers;

namespace ticket_reservation_platform
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; set; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.InstallServices(Configuration);
            services.AddHealthChecks();
            services.AddRequestTimeouts(options =>
            {
                options.DefaultPolicy = new RequestTimeoutPolicy
                {
                    Timeout = TimeSpan.FromSeconds(3)
                };
            });
        }

        public void Configure(IApplicationBuilder app)
        {  
            app
                .UseDefaultLocalization()
                .UseSwagger()
                .UseSwaggerUI()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/health/live");
                    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                    {
                        Predicate = healthCheck => healthCheck.Tags.Contains("ready"),
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                });
        }
    }
}
