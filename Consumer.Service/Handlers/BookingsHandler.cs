using System;
using System.Threading.Tasks;
using Consumer.Service.Handlers.Interfaces;
using Domain.Events;

namespace Consumer.Service.Handlers
{
    public class BookingsHandler : IBookingsHandler
    {
        public async Task HandleClassBooking(BookClassEvent evt)
        {
            Console.WriteLine(evt.ClassId);
        }
    }
}