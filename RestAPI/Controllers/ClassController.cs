using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Events;
using Domain.Services.Class;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories.Classes.Models;

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
        public HttpStatusCode BookClass(BookClassEvent evt)
        {
            _classService.BookClass(evt);
            return HttpStatusCode.OK;
        }
        
        [HttpGet]
        [Route("/getClass/{classId}")]
        public async Task<ClassReturnModel> GetClass(string classId)
        {
            return await _classService.GetClass(classId);
        }
        
        [HttpGet]
        [Route("/getClasses/{fitnessName}")]
        public async Task<List<ClassReturnModel>> GetAllClasses(string fitnessName)
        {
            return await _classService.GetClasses(fitnessName);
        }

        [HttpGet]
        [Route("/getUserClasses/{userId}")]
        public async Task<List<ClassReturnModel>> GetUserClasses(string userId)
        {
            return await _classService.GetUserClasses(userId);
        }

    }
}