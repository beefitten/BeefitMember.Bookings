using System;

namespace Persistence.Repositories.Classes
{
    public record ClassModel(
        string FitnessName,
        string ClassName,
        string ClassType,
        int MaxParticipants,
        int NumberOfParticipants,
        string ClassTimeStamp);
}