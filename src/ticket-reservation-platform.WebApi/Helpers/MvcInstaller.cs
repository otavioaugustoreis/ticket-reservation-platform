using Microsoft.AspNetCore.Mvc;

namespace ticket_reservation_platform.Helpers
{
    public static class MvcInstaller
    {
        public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore(setupAction => 
            {
                setupAction.Filters.Add<ApiGlobalExceptionFilterAttribute>();
                setupAction.RespectBrowserAcceptHeader = true;                  
            })
            .AddApiExplorer();
            
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddAllExtensions(configuration);

            return services;
        }
    }
}
