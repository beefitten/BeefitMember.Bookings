using Domain.Services;
using Domain.Services.Class;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBus, MessageBus>();
            services.AddSingleton<IClassService, ClassService>();
            //services.AddTransient<IFitnessService, FitnessService>();
            
            return services;
        }
    }
}