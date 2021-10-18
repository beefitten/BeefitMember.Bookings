using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories.Classes
{
    public interface IClassesRepository
    {
        Task AddClass(ClassModel model);
        Task<ClassReturnModel> GetClassInformation(Guid classId);
        Task<List<ClassReturnModel>> GetClasses(string fitnessName);

        Task AddBookingOnClass(Guid classId,
            bool isClassFull,
            int maxParticipants,
            int numberOfParticipants);
    }
}