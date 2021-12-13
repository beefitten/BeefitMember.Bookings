using System;
using System.Collections.Generic;

namespace Persistence.Repositories.Classes.Models
{
    public record ClassReturnModel(
        string ClassId,
        string FitnessName,
        string ClassName,
        string ClassType,
        string ClassImage,
        bool IsFull,
        int MaxParticipants,
        int NumberOfParticipants,
        DateTime TimeStart,
        DateTime TimeEnd,
        List<string> Participants,
        string Location);
}