using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Persistence.Repositories.Fitness
{
    public class FitnessRepository : IFitnessRepository
    {
        public async Task AddFitnessCenter(FitnessModel model)
        {
            var sql = @"INSERT INTO [dbo].[FitnessInformation] (FitnessName, Address, OpeningHours, Email, PhoneNumber) 
                        VALUES (@FitnessName, @Address, @OpeningHours, @Email, @PhoneNumber);";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@FitnessName", model.FitnessName);
            command.Parameters.AddWithValue("@Address", model.Address);
            command.Parameters.AddWithValue("@OpeningHours", model.OpeningHours);
            command.Parameters.AddWithValue("@Email", model.Email);
            command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);

            await Startup.InsertAsync(command);
        }

        public async Task<FitnessModel> GetFitnessCenterInformation(string name)
        {
            var sql = "SELECT * FROM  [dbo].[FitnessInformation] WHERE FitnessName = @FitnessName;";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@FitnessName", name);

            return await Startup.QueryFitnessModelAsync(command);
        }
    }
}