using System.Threading.Tasks;
using Domain.Events;
using Domain.Services;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    public class BookTrainerService : IBookTrainerService
    {
        private readonly MessageBus _messageBus;
        public BookTrainerService()
        {
            
        }

        public async Task BookTrainer(BookTrainerEvent evt)
        {
            
        }
    }
}