using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Classes;
using Persistence.Repositories.Fitness;

namespace Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services
                .AddTransient<IFitnessRepository, FitnessRepository>()
                .AddTransient<IClassesRepository, ClassesRepository>();

            return services;
        }
    }
}