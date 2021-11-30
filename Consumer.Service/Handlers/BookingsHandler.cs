using System;
using System.Threading.Tasks;
using Consumer.Service.Handlers.Interfaces;
using Domain.Events;
using Persistence.Repositories.Classes;
using Persistence.Repositories.FireStore;

namespace Consumer.Service.Handlers
{
    public class BookingsHandler : IBookingsHandler
    {
        private readonly IClassesRepository _repository;
        private readonly IFireStore _fireStore;

        public BookingsHandler(IClassesRepository repository, IFireStore fireStore)
        {
            _repository = repository;
            _fireStore = fireStore;
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
                await Task.WhenAll(
                    _repository.AddBookingOnClass(evt.ClassId,
                        true,
                        newNumberOfParticipants,
                        evt.Email),
                    _fireStore.AddUserToClass(evt.ClassId, evt.Email));
                
                Console.WriteLine("Booking added and class " + response.ClassName + " is now full!");
            }
            else
            {
                await Task.WhenAll(
                    _repository.AddBookingOnClass(evt.ClassId,
                        false,
                        newNumberOfParticipants,
                        evt.Email),
                    _fireStore.AddUserToClass(evt.ClassId, evt.Email));
                
                Console.WriteLine("Class " + response.ClassName + " have a new booking!");
            }
        }
    }
}