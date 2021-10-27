using System;
using System.Threading.Tasks;
using Consumer.Service.Handlers.Interfaces;
using Domain.Events;
using Domain.Setup;
using Persistence.Repositories.Classes;

namespace Consumer.Service.Handlers
{
    public class BookingsHandler : IBookingsHandler
    {
        private readonly IClassesRepository _repository;
        private readonly IFirebase _firebase;

        public BookingsHandler(IClassesRepository repository, IFirebase firebase)
        {
            _repository = repository;
            _firebase = firebase;
        }

        public async Task HandleClassBooking(BookClassEvent evt)
        {
            Console.WriteLine("Attempting to book on classId: " + evt.ClassId);

            var response = await _repository.GetClassInformation(evt.ClassId);

            if (response == null)
            {
                Console.WriteLine("No class found with classId: " + evt.ClassId);
                return;
            }

            if (response.IsFull)
            {
                Console.WriteLine("Class " + response.ClassName + " is full");
                return;
            }
            
            int newNumberOfParticipants = response.NumberOfParticipants + 1;

            if (newNumberOfParticipants == response.MaxParticipants)
            {
                await _repository.AddBookingOnClass(evt.ClassId,
                    true, 
                    newNumberOfParticipants,
                    evt.Email);
                
                await _firebase.AddUserToClass(evt.ClassId, evt.Email);
                
                Console.WriteLine("Booking added and class " + response.ClassName + " is now full!");
            }
            else
            {
                await _repository.AddBookingOnClass(evt.ClassId, 
                    false, 
                    newNumberOfParticipants,
                    evt.Email);
                
                await _firebase.AddUserToClass(evt.ClassId, evt.Email);
                
                Console.WriteLine("Class " + response.ClassName + " have a new booking!");
            }
        }
    }
}