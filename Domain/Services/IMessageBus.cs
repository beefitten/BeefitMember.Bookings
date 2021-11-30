using Domain.Events;

namespace Domain.Services
{
    public interface IMessageBus
    {
        void SendClassBookingMessage(BookClassEvent message);
    }
}