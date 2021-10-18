using System;
using System.Threading.Tasks;
using Consumer.Service.Handlers.Interfaces;
using Domain.Events;
using Persistence.Repositories.Classes;

namespace Consumer.Service.Handlers
{
    public class BookingsHandler : IBookingsHandler
    {
        private readonly IClassesRepository _repository;

        public BookingsHandler(IClassesRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleClassBooking(BookClassEvent evt)
        {
            Console.WriteLine("Booking is getting handled");

            var response = await _repository.GetClassInformation(evt.ClassId);

            if (response.IsClassFull)
            {
                Console.WriteLine("Class is full");
                return;
            }
            int newNumberOfParticipants = response.NumberOfParticipants + 1;

            if (newNumberOfParticipants == response.MaxParticipants)
            {
                await _repository.AddBookingOnClass(evt.ClassId,
                    true,
                    response.MaxParticipants, 
                    newNumberOfParticipants);
                
                Console.WriteLine("Booking added and class is full now!");
            }
            else
            {
                await _repository.AddBookingOnClass(evt.ClassId, 
                    false,
                    response.MaxParticipants, 
                    newNumberOfParticipants);
                
                Console.WriteLine("Booking added!");
            }
        }
    }
}