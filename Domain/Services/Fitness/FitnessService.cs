using System.Threading.Tasks;
using Persistence.Repositories;
using Persistence.Repositories.Fitness;

namespace Domain.Services.Fitness
{
    public class FitnessService : IFitnessService
    {
        private readonly IFitnessRepository _repository;

        public FitnessService(IFitnessRepository repository)
        {
            _repository = repository;
        }

        public async Task AddFitnessCenter(FitnessModel model)
        {
            await _repository.AddFitnessCenter(model);
        }

        public async Task<FitnessModel> GetFitnessInformation(string name)
        {
            return await _repository.GetFitnessCenterInformation(name);
        }
    }
}