
using ticket_reservation_platform.Helpers;

namespace ticket_reservation_platform.Helpers
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddAllExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .InitializeApplicationServices(configuration)
                .AddUseCasesExtensions(configuration)
                .AddProducers(configuration);

            return services;
        }

        public static IServiceCollection InitializeApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddUseCasesExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddProducers(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
