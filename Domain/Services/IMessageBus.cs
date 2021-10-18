using System.Threading.Tasks;
using Domain.Events;

namespace Domain.Services
{
    public interface IMessageBus
    {
        Task SendClassBookingMessage(BookClassEvent message);
        Task SendTrainerBookingMessage(BookTrainerEvent message);
    }
}