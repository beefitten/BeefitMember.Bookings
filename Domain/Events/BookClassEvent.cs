using System;

namespace Domain.Events
{
    public class BookClassEvent
    {
        public string ClassId { get; set; }
        public string Email { get; set; }
    }
}