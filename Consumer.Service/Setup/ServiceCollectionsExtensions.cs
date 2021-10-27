using Consumer.Service.Handlers;
using Consumer.Service.Handlers.Interfaces;
using Domain.Setup;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Classes;

namespace Consumer.Service.Setup
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddBookingsHandler(this IServiceCollection services)
        {
            services
                .AddTransient<IBookingsHandler, BookingsHandler>()
                .AddTransient<IClassesRepository, ClassesRepository>()
                .AddTransient<IFirebase, Firebase>();
            
            return services;
        }
    }
}