using System.Threading.Tasks;

namespace Persistence.Repositories.Fitness
{
    public interface IFitnessRepository
    {
        Task AddFitnessCenter(FitnessModel model);
        Task<FitnessModel> GetFitnessCenterInformation(string name);
    }
}