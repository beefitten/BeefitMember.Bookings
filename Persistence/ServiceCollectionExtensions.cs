using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Bookings;
using Persistence.Settings;

namespace Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services
                .AddTransient<IDatabaseSettings, BookingsSettings>()
                .AddTransient<IClassBookingsRepository, ClassBookingsRepository>();

            return services; 
        }
    }
}