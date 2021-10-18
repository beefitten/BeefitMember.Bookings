using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Events;
using Domain.Services;
using Domain.Services.Class;
using Domain.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Classes;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("Class")]
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
        
        [HttpPost]
        [Route("/addClass")]
        public async Task<HttpStatusCode> BookClass(ClassModel model)
        {
            await _classService.AddClass(model);
            return HttpStatusCode.OK;
        }
        
        [HttpPost]
        [Route("/bookClass")]
        public async Task<HttpStatusCode> BookClass(BookClassEvent evt)
        {
            await _classService.BookClass(evt);
            return HttpStatusCode.OK;
        }
        
        [HttpGet]
        [Route("/getClasses")]
        public async Task<List<ClassReturnModel>> GetAllClasses(string fitnessName)
        {
            return await _classService.GetClasses(fitnessName);
        }
    }
}