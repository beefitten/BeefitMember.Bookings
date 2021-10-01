using System;

namespace Domain.Events
{
    public class BookTrainerEvent
    {
        public Guid UserId { get; set; }
        public Guid TrainerId { get; set; }
        public Guid FitnessId { get; set; }
    }
}