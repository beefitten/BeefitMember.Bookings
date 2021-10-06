using System.Threading.Tasks;
using Domain.Events;
using Domain.Services;
using Domain.Services.Interfaces;
using Domain.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Services
{
    public class BookClassService : IBookClassService
    {
        private readonly IMessageBus _messageBus; 
        
        public BookClassService()
        {
            var fixture = new FixtureData();
            _messageBus = fixture.ServiceProvider.GetService<IMessageBus>();

        }
        
        public async Task BookClass(BookClassEvent evt)
        {
            _messageBus.SendClassBookingMessage(evt);

        }
    }
}