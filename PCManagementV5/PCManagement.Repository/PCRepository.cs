using System.Text;
using Npgsql;
using PCManagement.Common;
using PCManagement.RepositoryCommon;
using PCManagement.WebAPI.Models;

namespace PCManagement.Repository
{
    public class PCRepository : IPCRepository
    {
        public const string TABLE_NAME = "\"PC\"";

        public async Task<bool> AddPCAsync(PC newPc)
        {
            try
            {
                string commandText = $"INSERT INTO {TABLE_NAME} (\"Id\", \"Name\", \"CpuModelName\", \"GpuModelName\") VALUES (@id, @name, @cpu, @gpu)";
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    using var cmd = new NpgsqlCommand(commandText, connection);

                    cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("name", newPc.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("cpu", newPc.CpuModelName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("gpu", newPc.GpuModelName ?? (object)DBNull.Value);

                    connection.Open();

                    var affectedRows = await cmd.ExecuteNonQueryAsync();
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
                    var commandText = "SELECT \"Id\", \"Name\", \"CpuModelName\", \"GpuModelName\" FROM \"PC\" WHERE \"Id\" = @id;";
                    using var cmd = new NpgsqlCommand(commandText, connection);
                    cmd.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    await connection.OpenAsync();

                    var reader = await cmd.ExecuteReaderAsync();

                    if (!reader.HasRows)
                    {
                        await connection.CloseAsync();
                        return false;
                    }

                    await connection.CloseAsync();

                    commandText = "UPDATE \"PC\" set \"Name\" = @name, \"CpuModelName\" = @cpu, \"GpuModelName\" = @gpu WHERE \"Id\" = @id;";
                    using var updateCommand = new NpgsqlCommand(commandText, connection);
                    updateCommand.Parameters.AddWithValue("name", updatedPC.Name ?? (object)DBNull.Value);
                    updateCommand.Parameters.AddWithValue("cpu", updatedPC.CpuModelName ?? (object)DBNull.Value);
                    updateCommand.Parameters.AddWithValue("gpu", updatedPC.GpuModelName ?? (object)DBNull.Value);
                    updateCommand.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    await connection.OpenAsync();
                    var affectedRows = await updateCommand.ExecuteNonQueryAsync();
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

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PC>> GetAllPCsAsync(Sorting sorting, PCFilter filter, Paging paging)
        {
            var pcs = new List<PC>();

            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;

                        var query = new StringBuilder("SELECT * FROM \"PC\" WHERE 1 = 1 ");

                        ApplyFilter(filter, query, command);
                        ApplySorting(sorting, query);
                        ApplyPaging(paging, query, command);

                        command.CommandText = query.ToString();

                        connection.Open();

                        var reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            pcs.Add(new PC()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                CpuModelName = reader["CpuModelName"].ToString()!,
                                GpuModelName = reader["GpuModelName"].ToString()!
                            });
                        }

                        connection.Close();
                        return pcs;
                    }
                }
            }
            catch (Exception)
            {
                return pcs;
            }
        }

        public async Task<PC> GetPCAsync(Guid id)
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
                        var reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            var pc = new PC
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                CpuModelName = reader["CpuModelName"].ToString()!,
                                GpuModelName = reader["GpuModelName"].ToString()!
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

        public bool TestDatabaseConnectionAsync()
        {
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    connection.Open();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ApplySorting(Sorting sorting, StringBuilder query)
        {
            if (sorting == null) return;
            query.Append($" ORDER BY \"{sorting.OrderBy}\" {sorting.SortOrder}");
        }

        private void ApplyFilter(PCFilter filter, StringBuilder query, NpgsqlCommand parameters)
        {
            if (filter == null) return;

            if (!string.IsNullOrWhiteSpace(filter.PCName))
            {
                query.Append(" AND \"Name\" ILIKE @Name");
                parameters.Parameters.AddWithValue("@name", $"%{filter.PCName}%");
            }

            if (!string.IsNullOrWhiteSpace(filter.CpuModelName))
            {
                query.Append(" AND \"CpuModelName\" ILIKE @CpuModelName");
                parameters.Parameters.AddWithValue("@CpuModelName", $"%{filter.CpuModelName}%");
            }

            if (!string.IsNullOrWhiteSpace(filter.GpuModelName))
            {
                query.Append(" AND \"GpuModelName\" ILIKE @GpuModelName");
                parameters.Parameters.AddWithValue("@GpuModelName", $"%{filter.GpuModelName}%");
            }
        }

        private void ApplyPaging(Paging paging, StringBuilder query, NpgsqlCommand command)
        {
            if (paging == null) return;

            query.Append(" OFFSET @OFFSET FETCH NEXT @ROWS ROWS ONLY;");
            command.Parameters.AddWithValue("@OFFSET", paging.PageNumber == 1 ? 0 : (paging.PageNumber - 1) * paging.Rpp);
            command.Parameters.AddWithValue("@ROWS", paging.Rpp);
        }
    }
}