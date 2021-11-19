using System;
using System.Threading.Tasks;

namespace Persistence.Repositories.FireStore
{
    public interface IFireStore
    {
        Task AddClass(Guid classId);
        Task DeleteBooking(string classId, string email);
        Task AddUserToClass(string classId, string email);
    }
}