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
            return await _classService.AddClass(model);
        }
        
        [HttpPost]
        [Route("/bookClass")]
        public HttpStatusCode BookClass(BookClassEvent evt)
        {
            _classService.BookClass(evt);
            return HttpStatusCode.OK;
        }
        
        [HttpPost]
        [Route("/deleteBooking")]
        public HttpStatusCode DeleteBooking(BookClassEvent evt)
        {
            _classService.DeleteBooking(evt);
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

        [HttpGet]
        [Route("/getRandomAppointments")]
        public List<AppointmentsModel> GetRandomAppointments()
        {
            return Appointments.GenerateData();
        }

        private class Appointments
        {
            public static List<AppointmentsModel> GenerateData()
            {
                var appointments = new List<AppointmentsModel>();

                var appointment = new AppointmentsModel(
                    "Personal Training Session",
                    "https://www.myfooddiary.com/blog/asset/2419/personal_training_session.jpg",
                    "On thursday at 15:40");
                
                appointments.Add(appointment);
                appointments.Add(appointment);
                appointments.Add(appointment);
                appointments.Add(appointment);

                return appointments;
            }
        }
        
        public record AppointmentsModel(string Headline, string Image, string Date);
    }
}