using System;

namespace Persistence.Repositories.Classes.Models
{
    public record ClassModel(
        string FitnessName,
        string ClassName,
        string ClassType,
        string ClassImage,
        int MaxParticipants,
        DateTime TimeStart,
        DateTime TimeEnd,
        string Location);
}