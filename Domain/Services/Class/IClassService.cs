using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Events;
using Persistence.Repositories.Classes;

namespace Domain.Services.Class
{
    public interface IClassService
    {
        Task AddClass(ClassModel model);
        Task BookClass(BookClassEvent evt);
        Task<List<ClassReturnModel>> GetClasses(string fitnessName);
    }
}