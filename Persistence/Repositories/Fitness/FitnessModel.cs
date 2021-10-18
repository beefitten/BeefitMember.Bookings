namespace Persistence.Repositories.Fitness
{
    public record FitnessModel(
        string FitnessName, 
        string Address,
        string OpeningHours,
        string Email,
        string PhoneNumber);
}