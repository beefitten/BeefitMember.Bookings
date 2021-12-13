using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Persistence.Repositories.Classes.Models;

namespace Persistence.Repositories.Classes
{
    public interface IClassesRepository
    {
        Task<HttpStatusCode> AddClass(ClassModel model, Guid classId);
        Task<ClassReturnModel> GetClassInformation(string classId);
        Task<List<ClassReturnModel>> GetClasses(string fitnessName);
        Task<List<ClassReturnModel>> GetUserClasses(string userId);
        Task AddBookingOnClass(
            string classId,
            bool isClassFull,
            int numberOfParticipants,
            string email);
        Task DeleteBooking(string classId, string email);
    }
}