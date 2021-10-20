using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Persistence.Repositories.Classes.Models
{
    public class ClassMongoModel
    {
        [BsonId]
        [BsonRequired]
        [BsonElement("ClassId")]
        public String ClassId { get; set; }
        
        [BsonRequired]
        [BsonElement("FitnessName")]
        public string FitnessName { get; set; }
        
        [BsonRequired]
        [BsonElement("ClassName")]
        public string ClassName { get; set; }
        
        [BsonRequired]
        [BsonElement("ClassType")]
        public string ClassType { get; set; }
        
        [BsonRequired]
        [BsonElement("ClassImage")]
        public string ClassImage { get; set; }
        
        [BsonElement("Location")]
        public string Location { get; set; }
        
        [BsonRequired]
        [BsonElement("IsFull")]
        public bool IsFull { get; set; }
        
        [BsonRequired]
        [BsonElement("MaxParticipants")]
        public int MaxParticipants { get; set; }
        
        [BsonElement("NumberOfParticipants")]
        public int NumberOfParticipants { get; set; }
        
        [BsonElement("TimeStart")]
        public DateTime TimeStart { get; set; }
        
        [BsonElement("TimeEnd")]
        public DateTime TimeEnd { get; set; }
        
        [BsonElement("Participants")]
        public List<string> Participants { get; set; }
    }
}