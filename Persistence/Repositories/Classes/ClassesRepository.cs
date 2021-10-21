using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MongoDB.Driver;
using Persistence.Repositories.Classes.Models;
using Persistence.Settings;

namespace Persistence.Repositories.Classes
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly IMongoCollection<ClassMongoModel> _classesCollection;
        
        public ClassesRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _classesCollection = database.GetCollection<ClassMongoModel>(settings.CollectionName);

            // var options = new CreateIndexOptions{Unique = false};
            // _classesCollection.Indexes.CreateOne(Builders<ClassMongoModel>.IndexKeys.Text(x=>x.Participants));
        }
        
        public async Task<HttpStatusCode> AddClass(ClassModel model)
        {
            if (model == null)
                throw new Exception("Model cannot be null!");

            try
            {
                var classMongoModel = new ClassMongoModel()
                {
                    ClassId = Guid.NewGuid().ToString(),
                    FitnessName = model.FitnessName,
                    ClassName = model.ClassName,
                    ClassType = model.ClassType,
                    ClassImage = model.ClassImage,
                    IsFull = false,
                    MaxParticipants = model.MaxParticipants,
                    NumberOfParticipants = 0,
                    TimeStart = model.TimeStart,
                    TimeEnd = model.TimeEnd,
                    Participants = new List<string>(),
                    Location = model.Location
                };
                
                await _classesCollection.InsertOneAsync(classMongoModel);
                return HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                return HttpStatusCode.Conflict;
            }
        }

        public async Task<ClassReturnModel> GetClassInformation(string classId)
        {
            if (classId == null)
                throw new Exception("ClassId cannot be null!");

            var model = await _classesCollection
                .Find<ClassMongoModel>(item => item.ClassId == classId)
                .FirstOrDefaultAsync();

            if (model == null)
                throw new Exception("Class not found!");

            return new ClassReturnModel(
                model.ClassId,
                model.FitnessName,
                model.ClassImage,
                model.ClassType,
                model.ClassImage,
                model.IsFull,
                model.MaxParticipants,
                model.NumberOfParticipants,
                model.TimeStart,
                model.TimeEnd,
                model.Participants,
                model.Location);
        }

        public async Task<List<ClassReturnModel>> GetClasses(string fitnessName)
        {
            var model = await _classesCollection
                .Find(item => item.FitnessName == fitnessName)
                .ToListAsync();

            var classReturnModel = new List<ClassReturnModel>();

            foreach (var item in model)
            {
                var classItem = new ClassReturnModel(
                        item.ClassId,
                        item.FitnessName,
                        item.ClassName,
                        item.ClassType,
                        item.ClassImage,
                        item.IsFull,
                        item.MaxParticipants,
                        item.NumberOfParticipants,
                        item.TimeStart,
                        item.TimeEnd,
                        item.Participants,
                        item.Location);

                classReturnModel.Add(classItem);
            }

            return classReturnModel;
        }

        public async Task<List<ClassReturnModel>> GetUserClasses(string userId)
        {
            var response = await _classesCollection.Find(Builders<ClassMongoModel>.Filter.Text(userId)).ToListAsync();

            var classModel = new List<ClassReturnModel>();

            foreach (var item in response)
            {
                var classReturnModel = new ClassReturnModel(
                    item.ClassId,
                    item.FitnessName,
                    item.ClassName,
                    item.ClassType,
                    item.ClassImage,
                    item.IsFull,
                    item.MaxParticipants,
                    item.NumberOfParticipants,
                    item.TimeStart,
                    item.TimeEnd,
                    item.Participants,
                    item.Location);
                
                classModel.Add(classReturnModel);
            }
            
            return classModel;
        }

        public async Task AddBookingOnClass(
            string classId,
            bool isClassFull, 
            int numberOfParticipants,
            string email)
        {
            var model = await _classesCollection
                .Find<ClassMongoModel>(item => item.ClassId == classId)
                .FirstOrDefaultAsync();

            if (model.Participants.Contains(email))
                return;

            model.Participants.Add(email);
            
            var filter = Builders<ClassMongoModel>.Filter.Eq("ClassId", classId);
            var update = Builders<ClassMongoModel>.Update
                .Set("Participants", model.Participants)
                .Set("IsFull", isClassFull)
                .Set("NumberOfParticipants", numberOfParticipants);

            await _classesCollection.UpdateOneAsync(filter, update);
        }
    }
}