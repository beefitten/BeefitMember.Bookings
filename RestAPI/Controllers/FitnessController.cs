using System.Threading.Tasks;
using Domain.Services.Fitness;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories.Fitness;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("Fitness")]
    public class FitnessController : Controller
    {
        private readonly IFitnessService _fitnessService;
        
        public FitnessController(IFitnessService fitnessService)
        {
            _fitnessService = fitnessService;
        }

        [HttpPost]
        [Route("/addFitness")]
        public async Task BookTrainer(FitnessModel model)
        {
            await _fitnessService.AddFitnessCenter(model);
        }
        
        [HttpGet]
        public async Task<FitnessModel> Get(string name)
        {
            return await _fitnessService.GetFitnessInformation(name);
        }
    }
}