using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Classes;
using Persistence.Settings;

namespace Persistence.Setup
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services
                .AddTransient<IDatabaseSettings, BookingSettings>()
                .AddTransient<IClassesRepository, ClassesRepository>();
            
            return services;
        }
    }
}