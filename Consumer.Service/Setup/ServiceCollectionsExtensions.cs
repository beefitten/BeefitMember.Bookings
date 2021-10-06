using Consumer.Service.Handlers;
using Consumer.Service.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Service.Setup
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddBookingsHandler(this IServiceCollection services)
        {
            services.AddSingleton<IBookingsHandler, BookingsHandler>();
            return services;
        }
    }
}