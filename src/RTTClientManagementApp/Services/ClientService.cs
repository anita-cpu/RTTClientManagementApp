using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RTTClientManagementApp.Models;

namespace RTTClientManagementApp.Services
{
    public class ClientService : IClientService
    {
        private readonly string _connectionString = "YourConnectionStringHere";

        public async Task AddClientAsync(ClientModel client)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insert client
                            var command = connection.CreateCommand();
                            command.Transaction = transaction;
                            command.CommandText = "INSERT INTO Clients (Name, Gender) VALUES (@Name, @Gender); SELECT SCOPE_IDENTITY();";
                            command.Parameters.AddWithValue("@Name", client.Name);
                            command.Parameters.AddWithValue("@Gender", client.Gender);

                            var clientId = Convert.ToInt32(await command.ExecuteScalarAsync());

                            // Insert addresses
                            foreach (var address in client.Addresses)
                            {
                                command.CommandText = "INSERT INTO Addresses (ClientId, Type, Street, City, State, ZipCode) VALUES (@ClientId, @Type, @Street, @City, @State, @ZipCode)";
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@ClientId", clientId);
                                command.Parameters.AddWithValue("@Type", address.Type);
                                command.Parameters.AddWithValue("@Street", address.Street);
                                command.Parameters.AddWithValue("@City", address.City);
                                command.Parameters.AddWithValue("@State", address.State);
                                command.Parameters.AddWithValue("@ZipCode", address.ZipCode);
                                await command.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("An error occurred while adding the client.", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error (implement logging as needed)
                throw new Exception("An error occurred while processing your request.", ex);
            }
        }


        private void ValidateClient(ClientModel client)
        {
            var context = new ValidationContext(client, null, null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(client, context, results, true))
            {
                foreach (var error in results)
                {
                    //////Logger.Warn(error.ErrorMessage);(requires installation of NLog , I used a macbook and visual studio which would not allow me to install )
                    throw new ArgumentException(error.ErrorMessage);
                }
            }
        }
    }
}
