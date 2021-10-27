using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Events;
using Persistence.Repositories.Classes.Models;

namespace Domain.Services.Class
{
    public interface IClassService
    {
        Task<HttpStatusCode> AddClass(ClassModel model);
        void BookClass(BookClassEvent evt);
        Task DeleteBooking(BookClassEvent evt);
        Task<ClassReturnModel> GetClass(string classId);
        Task<List<ClassReturnModel>> GetClasses(string fitnessName);
        Task<List<ClassReturnModel>> GetUserClasses(string userId);
    }
}