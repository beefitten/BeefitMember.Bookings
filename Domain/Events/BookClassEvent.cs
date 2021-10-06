using System;

namespace Domain.Events
{
    public class BookClassEvent
    {
        public Guid UserId { get; set; }
        public Guid ClassId { get; set; }
        public Guid FitnessId { get; set; }
    }
}