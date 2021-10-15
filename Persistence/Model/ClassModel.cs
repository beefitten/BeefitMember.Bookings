using System;

namespace Persistence.Model
{
    public class ClassModel
    {
        public Guid ClassId { get; set; }
        public int MaxParticipants { get; set; }
        public String[] Participants { get; set; }
    }
}