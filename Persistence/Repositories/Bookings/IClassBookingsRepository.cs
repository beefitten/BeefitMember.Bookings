using System;
using System.Net;
using System.Threading.Tasks;

namespace Persistence.Repositories.Bookings
{
    public interface IClassBookingsRepository
    {
        Task<HttpStatusCode> MakeReservation(Guid classId);
    }
}