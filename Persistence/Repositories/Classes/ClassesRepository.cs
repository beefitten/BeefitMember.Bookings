using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Persistence.Repositories.Classes
{
    public class ClassesRepository : IClassesRepository
    {
        public async Task AddClass(ClassModel model)
        {
            var sql = @"INSERT INTO [dbo].[ClassInformation] 
            (ClassId, FitnessName, ClassName, ClassType, IsClassFull, MaxParticipants, NumberOfParticipants, ClassTimeStamp) 
            VALUES (@ClassId, @FitnessName, @ClassName, @ClassType, @IsClassFull, @MaxParticipants, @NumberOfParticipants, @ClassTimeStamp);";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@ClassId", Guid.NewGuid());
            command.Parameters.AddWithValue("@FitnessName", model.FitnessName);
            command.Parameters.AddWithValue("@ClassName", model.ClassName);
            command.Parameters.AddWithValue("@ClassType", model.ClassType);
            command.Parameters.AddWithValue("@IsClassFull", false);
            command.Parameters.AddWithValue("@MaxParticipants", model.MaxParticipants);
            command.Parameters.AddWithValue("@NumberOfParticipants", model.NumberOfParticipants);
            command.Parameters.AddWithValue("@ClassTimeStamp", model.ClassTimeStamp);
            
            await Startup.InsertAsync(command);
        }

        public async Task<ClassReturnModel> GetClassInformation(Guid classId)
        {
            var sql = "SELECT * FROM [dbo].[ClassInformation] WHERE classId = @classId;";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@classId", classId);

            return await Startup.QueryClassModelAsync(command);
        }

        public Task<List<ClassReturnModel>> GetClasses(string fitnessName)
        {
            var sql = "SELECT * FROM [dbo].[ClassInformation] WHERE fitnessName = @fitnessName;";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@fitnessName", fitnessName);

            return Startup.QueryAllClassesAsync(command);
        }

        public async Task AddBookingOnClass(Guid classId,
            bool isClassFull, 
            int maxParticipants, 
            int numberOfParticipants)
        {
            var sql = @"UPDATE [dbo].[ClassInformation] 
                        SET IsClassFull = @IsClassFull, MaxParticipants = @MaxParticipants, NumberOfParticipants = @NumberOfParticipants 
                        WHERE classId = @classId;";
            
            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@classId", classId);
            command.Parameters.AddWithValue("@IsClassFull", isClassFull);
            command.Parameters.AddWithValue("@MaxParticipants", maxParticipants);
            command.Parameters.AddWithValue("@NumberOfParticipants", numberOfParticipants);

            await Startup.InsertAsync(command);
        }
    }
}