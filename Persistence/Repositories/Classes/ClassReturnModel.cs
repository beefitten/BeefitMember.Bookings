using System;

namespace Persistence.Repositories.Classes
{
    public record ClassReturnModel(
        Guid ClassId,
        string FitnessName,
        string ClassName,
        string ClassType,
        bool IsClassFull,
        int MaxParticipants,
        int NumberOfParticipants,
        string ClassTimeStamp);
}