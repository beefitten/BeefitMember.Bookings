using System.Threading.Tasks;
using Domain.Events;
using Domain.Services.Interfaces;
using Domain.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("Bookings")]
    public class BookingsController : Controller
    {
        private readonly IMessageBus _MessageBus;
        
        public BookingsController()
        {
            var fixture = new FixtureData();
            _MessageBus = fixture.ServiceProvider.GetService<IMessageBus>();
        }
        
        [HttpPost]
        [Route("/bookTrainer")]
        public async Task BookTrainer(BookTrainerEvent evt)
        {
            _MessageBus.SendTrainerBookingMessage(evt);
        }

        [HttpPost]
        [Route("/bookClass")]
        public async Task BookClass(BookClassEvent evt)
        {
            _MessageBus.SendClassBookingMessage(evt);
        }

    }
}