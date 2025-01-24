using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using PCManagement.Common;
using PCManagement.WebAPI.Models;


namespace PCManagement.Repository
{
    public class PCRepository
    {
        public const string TABLE_NAME = "\"PC\"";
        public async Task<bool> AddPCAsync(PC newPc)
        {
            try
            {
                string commandText = $"INSERT INTO {TABLE_NAME} (\"Id\", \"Name\", \"CPU\", \"GPU\") VALUES (@id, @name, @cpu, @gpu)";
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    using var cmd = new NpgsqlCommand(commandText, connection);

                    cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("name", newPc.Name);
                    cmd.Parameters.AddWithValue("cpu", newPc.CPU);
                    cmd.Parameters.AddWithValue("gpu", newPc.GPU);

                    await connection.OpenAsync();

                    var affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows == 0)
                        return false;

                    await connection.CloseAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePCAsync(Guid id, PC updatedPC)
        {
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    var commandText = "SELECT \"Id\", \"Name\", \"CPU\", \"GPU\" FROM \"PC\" WHERE \"Id\" = @id;";
                    using var cmd = new NpgsqlCommand(commandText, connection);
                    cmd.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    await connection.OpenAsync();

                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return false;
                    }

                    await connection.CloseAsync();

                    commandText = "UPDATE \"PC\" set \"Name\" = @name, \"CPU\" = @cpu, \"GPU\" = @gpu WHERE \"Id\" = @id;";
                    using var updateCommand = new NpgsqlCommand(commandText, connection);
                    updateCommand.Parameters.AddWithValue("name", updatedPC.Name);
                    updateCommand.Parameters.AddWithValue("cpu", updatedPC.CPU);
                    updateCommand.Parameters.AddWithValue("gpu", updatedPC.GPU);
                    updateCommand.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    await connection.OpenAsync();
                    var affectedRows = updateCommand.ExecuteNonQuery();
                    await connection.CloseAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePCAsync(Guid id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    var commandText = "DELETE FROM \"PC\" WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    await connection.OpenAsync();

                    var affectedRows = command.ExecuteNonQuery();
                    await connection.CloseAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PC>> GetAllPCsAsync()
        {
            var pcs = new List<PC>();
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    var commandText = "SELECT * FROM \"PC\"";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        await connection.OpenAsync();

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var pc = new PC
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                CPU = reader["CPU"].ToString()!,
                                GPU = reader["GPU"].ToString()!
                            };
                            pcs.Add(pc);
                        }

                        await connection.CloseAsync();
                        return pcs;
                    }
                }
            }
            catch (Exception)
            {
                return pcs;
            }
        }   

        public async Task<PC?> GetPCAsync(Guid id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    var commandText = "SELECT * FROM \"PC\" WHERE \"Id\" = @id;";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                        await connection.OpenAsync();
                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            var pc = new PC
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                CPU = reader["CPU"].ToString()!,
                                GPU = reader["GPU"].ToString()!
                            };

                            await connection.CloseAsync();
                            return pc;
                        }

                        await connection.CloseAsync();
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> TestDatabaseConnectionAsync()
        {
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    await connection.OpenAsync();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
