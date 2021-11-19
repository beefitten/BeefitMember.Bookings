using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Events;
using Domain.Setup;
using Persistence.Repositories.Classes;
using Persistence.Repositories.Classes.Models;
using Persistence.Repositories.FireStore;

namespace Domain.Services.Class
{
    public class ClassService : IClassService
    {
        private readonly IMessageBus _messageBus;
        private readonly IClassesRepository _repository;
        private readonly IFireStore _fireStore;
        
        public ClassService(
            IMessageBus messageBus, 
            IClassesRepository repository, 
            IFireStore fireStore)
        {
            _messageBus = messageBus;
            _repository = repository;
            _fireStore = fireStore;
        }

        public async Task<HttpStatusCode> AddClass(ClassModel model)
        {
            var classId = Guid.NewGuid();

            await _fireStore.AddClass(classId);
            
            return await _repository.AddClass(model, classId);
        }

        public void BookClass(BookClassEvent evt)
        {
            _messageBus.SendClassBookingMessage(evt);
        }

        public Task DeleteBooking(BookClassEvent evt)
        {
            _repository.DeleteBooking(evt.ClassId, evt.Email);
            return _fireStore.DeleteBooking(evt.ClassId, evt.Email);
        }

        public async Task<ClassReturnModel> GetClass(string classId)
        {
            return await _repository.GetClassInformation(classId);
        }

        public async Task<List<ClassReturnModel>> GetClasses(string fitnessName)
        {
            return await _repository.GetClasses(fitnessName);
        }

        public async Task<List<ClassReturnModel>> GetUserClasses(string userId)
        {
            return await _repository.GetUserClasses(userId);
        }
    }
}