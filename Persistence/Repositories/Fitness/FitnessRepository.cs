using System.Data.SqlClient;
using System.Threading.Tasks;
using Persistence.Repositories.Classes;

namespace Persistence.Repositories.Fitness
{
    public class FitnessRepository : IFitnessRepository
    {
        public async Task AddFitnessCenter(FitnessModel model)
        {
            var sql = "INSERT INTO [dbo].[FitnessInformation] (FitnessName, Address) VALUES (@FitnessName, @Address);";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@FitnessName", model.FitnessName);
            command.Parameters.AddWithValue("@Address", model.Address);

            await Startup.InsertAsync(command);
        }

        public async Task<string> GetFitnessCenterInformation(string name)
        {
            var sql = "SELECT * FROM  [dbo].[FitnessInformation] WHERE FitnessName = @FitnessName;";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@FitnessName", name);

            return "Not implemented"; //await Startup.QueryAsync(command);
        }
    }
}