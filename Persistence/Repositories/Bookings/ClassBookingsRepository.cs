using System;
using System.Net;
using System.Threading.Tasks;
using MongoDB.Driver;
using Persistence.Model;
using Persistence.Settings;

namespace Persistence.Repositories.Bookings
{
    public class ClassBookingsRepository : IClassBookingsRepository
    {
        private readonly IMongoCollection<ClassModel> _collection;

        public ClassBookingsRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<ClassModel>(settings.CollectionName);
        }

        public Task<HttpStatusCode> MakeReservation(ClassBookingModel model)
        {
            // if (model == null)
            //     throw new Exception("Empty model is not allowed");
            //
            // var classToBook = _collection.FindAsync(c => c.ClassId == model.ClassId);
            // if (classToBook == null)
            //     return HttpStatusCode.OK();
        }
    }
}