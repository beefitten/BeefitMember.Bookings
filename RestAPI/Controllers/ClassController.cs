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
        public async Task<IActionResult> BookClass(ClassModel model)
        {
            var response = await _classService.AddClass(model);

            if (response == HttpStatusCode.OK)
                return Ok();

            return Conflict();
        }
        
        [HttpPost]
        [Route("/bookClass")]
        public IActionResult BookClass(BookClassEvent evt)
        {
            _classService.BookClass(evt);
            return Ok();
        }
        
        [HttpDelete]
        [Route("/deleteBooking")]
        public IActionResult DeleteBooking(BookClassEvent evt)
        {
            _classService.DeleteBooking(evt);
            return Ok();
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
                    "https://live.staticflickr.com/8044/8376983917_9182dcc993_b.jpg",
                    "On thursday at 15:40");
                    
                var appointment2 = new AppointmentsModel(
                    "Personal Training Session",
                    "https://live.staticflickr.com/4078/4914138831_83c09bc311_m.jpg",
                    "On friday at 10:00");
                
                var appointment3 = new AppointmentsModel(
                    "Personal Training Session",
                    "https://live.staticflickr.com/8044/8376983917_9182dcc993_b.jpg",
                    "On 22/10/2021 at 10:00");
                
                var appointment4 = new AppointmentsModel(
                    "Personal Training Session",
                    "https://live.staticflickr.com/4078/4914138831_83c09bc311_m.jpg",
                    "On 27/10/2021 at 10:00");
                
                appointments.Add(appointment);
                appointments.Add(appointment2);
                appointments.Add(appointment3);
                appointments.Add(appointment4);

                return appointments;
            }
        }
        
        public record AppointmentsModel(string Headline, string Image, string Date);
    }
}