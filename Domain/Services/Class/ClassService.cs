using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Events;
using MongoDB.Bson;
using Persistence.Repositories.Classes;
using Persistence.Repositories.Classes.Models;

namespace Domain.Services.Class
{
    public class ClassService : IClassService
    {
        private readonly IMessageBus _messageBus;
        private readonly IClassesRepository _repository;
        
        public ClassService(IMessageBus messageBus, IClassesRepository repository)
        {
            _messageBus = messageBus;
            _repository = repository;
        }

        public async Task<HttpStatusCode> AddClass(ClassModel model)
        {
            return await _repository.AddClass(model);
        }

        public void BookClass(BookClassEvent evt)
        {
            _messageBus.SendClassBookingMessage(evt);
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