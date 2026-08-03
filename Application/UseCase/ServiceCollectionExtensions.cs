using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCase
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<Customer.ISaveCustomerUseCase, Customer.SaveCustomerUseCase>();

            return services;
        }
    }
}
