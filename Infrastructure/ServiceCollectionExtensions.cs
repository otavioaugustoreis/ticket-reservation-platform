using Domain.Abstractions;
using Infrastructure.Abstractions;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SqlInstaller(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlServerOptionsConfiguration>(configuration.GetSection(SqlServerOptionsConfiguration.Section));
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

            services.AddScoped<UnitOfWork>();
            services.AddScoped<IDbSession>(sp => sp.GetRequiredService<UnitOfWork>());
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UnitOfWork>());

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            
            return services;
        }
    }
}
