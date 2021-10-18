using System;

namespace Persistence.Repositories.Classes
{
    public record ClassModel(
        Guid ClassId,
        string FitnessName,
        string ClassName,
        string ClassType,
        int MaxParticipants,
        int NumberOfParticipants,
        string ClassTimeStamp);
}