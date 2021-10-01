using System.Threading.Tasks;
using Domain.Events;

namespace Consumer.Service.Handlers.Interfaces
{
    public interface IBookingsHandler
    {
        Task HandleClassBooking(BookClassEvent evt);
    }
}