using System;

namespace Persistence.Model
{
    public class ClassBookingModel
    {
        public Guid ClassId { get; set; }
        public Guid RequesteeId { get; set; }
        public string RequesteeName { get; set; }
    }
}