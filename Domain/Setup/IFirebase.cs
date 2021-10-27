using System;
using System.Threading.Tasks;

namespace Domain.Setup
{
    public interface IFirebase
    {
        Task AddClass(Guid classId);
        Task DeleteBooking(string classId, string email);
        Task AddUserToClass(string classId, string email);
    }
}