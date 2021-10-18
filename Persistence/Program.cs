using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Persistence.Repositories.Classes;

namespace Persistence
{
    static class Startup
    {
        static async Task Main(string[] args)
        {
            try 
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "localhost",
                    UserID = "SA",
                    Password = "yourStrong(!)Password",
                    InitialCatalog = "Fitness"
                };
            
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    
                    // Runs all SQL Scripts
                    await RunSqlScripts(connection);
                    
                    Console.WriteLine("Success");
                    
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

        private static async Task RunSqlScripts(SqlConnection connection)
        {
            // // Creating DB
            // var sql = "CREATE DATABASE Fitness;";
            // var command = new SqlCommand(sql, connection);
            // await command.ExecuteNonQueryAsync();
                    
            // Run SQL
            // var script = await File.ReadAllTextAsync(@"D:\Skole\7. semester\Bachelor\Git\BeefitMember.Bookings\Persistence\SQL\001CreateFitnessTable.sql");
            // var createTables = new SqlCommand(script, connection);
            // await createTables.ExecuteNonQueryAsync();
                    
            var script2 = await File.ReadAllTextAsync(@"D:\Skole\7. semester\Bachelor\Git\BeefitMember.Bookings\Persistence\SQL\002AddClassTable.sql");
            var d = new SqlCommand(script2, connection);
            await d.ExecuteNonQueryAsync();
        }

        public static async Task InsertAsync(SqlCommand sqlCommand)
        {
            try 
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "localhost",
                    UserID = "SA",
                    Password = "yourStrong(!)Password",
                    InitialCatalog = "Fitness"
                };

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    sqlCommand.Connection = connection;
                    
                    var response = await sqlCommand.ExecuteNonQueryAsync();
                    
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        public static async Task<ClassReturnModel> QueryAsyncClassModel(SqlCommand sqlCommand)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "localhost",
                    UserID = "SA",
                    Password = "yourStrong(!)Password",
                    InitialCatalog = "Fitness"
                };

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    sqlCommand.Connection = connection;

                    var reader = await sqlCommand.ExecuteReaderAsync();
                    
                    while (await reader.ReadAsync())
                    {
                        var classId = reader.GetValue(0).ToString();
                        var fitnessName = reader.GetValue(1).ToString();
                        var className = reader.GetValue(2).ToString();
                        var classType = reader.GetValue(3).ToString();
                        var isClassFull = reader.GetValue(4).ToString();
                        var maxParticipants = reader.GetValue(5).ToString();
                        var numberOfParticipants = reader.GetValue(6).ToString();
                        var timeStamp = reader.GetValue(7).ToString();
                        return new ClassReturnModel(new Guid(classId),
                            fitnessName,
                            className,
                            classType,
                            bool.Parse(isClassFull),
                            int.Parse(maxParticipants),
                            int.Parse(numberOfParticipants),
                            timeStamp);
                    }
                    
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }
        
        public static async Task<List<ClassReturnModel>> QueryAsyncAllClasses(SqlCommand sqlCommand)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "localhost",
                    UserID = "SA",
                    Password = "yourStrong(!)Password",
                    InitialCatalog = "Fitness"
                };

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    sqlCommand.Connection = connection;

                    var reader = await sqlCommand.ExecuteReaderAsync();
                    List<ClassReturnModel> classes = new List<ClassReturnModel>();
                    
                    while (await reader.ReadAsync())
                    {
                        var classId = reader.GetValue(0).ToString();
                        var fitnessName = reader.GetValue(1).ToString();
                        var className = reader.GetValue(2).ToString();
                        var classType = reader.GetValue(3).ToString();
                        var isClassFull = reader.GetValue(4).ToString();
                        var maxParticipants = reader.GetValue(5).ToString();
                        var numberOfParticipants = reader.GetValue(6).ToString();
                        var timeStamp = reader.GetValue(7).ToString();
                        
                        var model = new ClassReturnModel(new Guid(classId),
                            fitnessName,
                            className,
                            classType,
                            bool.Parse(isClassFull),
                            int.Parse(maxParticipants),
                            int.Parse(numberOfParticipants),
                            timeStamp);
                        
                        classes.Add(model);
                    }

                    connection.Close();
                    return classes;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }
    }
}