using Domain.Services;
using Domain.Services.Class;
using Domain.Setup;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.FireStore;

namespace Domain
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBus, MessageBus>();
            services.AddSingleton<IClassService, ClassService>();
            services.AddSingleton<IFireStore, FireStore>();
            
            return services;
        }
    }
}