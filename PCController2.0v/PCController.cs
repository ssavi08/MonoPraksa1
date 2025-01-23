using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using PCManagement.WebAPI.Models;

namespace PCManagement.WebAPI.Controllers
{
    public static class DatabaseConfig
    {
        public const string connString =
            "Host=localhost:5432;" +
            "Username=postgres;" +
            "Password=12345;" +
            "Database=PCDataBase";
    }

    public class NpgsqlPCManagementRepository
    {
        private readonly NpgsqlConnection _connection;

        public NpgsqlPCManagementRepository()
        {
            _connection = new NpgsqlConnection(DatabaseConfig.connString);
            _connection.Open();
        }

        public const string TABLE_NAME = "\"PC\"";

    }

    [Route("api/[controller]")]
    [ApiController]
    public class PCController : ControllerBase
    {
        public const string TABLE_NAME = "\"PC\"";
        private readonly NpgsqlPCManagementRepository _repository;

        public PCController()
        {
            _repository = new NpgsqlPCManagementRepository();
        }

        [HttpPost]
        public IActionResult Add([FromBody]PC pc)
        {
            if (pc == null)
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = "Invalid data."
                });

            try
            {
                string commandText = $"INSERT INTO {TABLE_NAME} (\"Id\", \"Name\", \"CPU\", \"GPU\") VALUES (@id, @name, @cpu, @gpu)";
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    using var cmd = new NpgsqlCommand(commandText, connection);
                    
                    cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("name", pc.Name);
                    cmd.Parameters.AddWithValue("cpu", pc.CPU);
                    cmd.Parameters.AddWithValue("gpu", pc.GPU);

                    connection.Open();

                    var affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows == 0)
                        return BadRequest();

                    connection.Close();

                    return Ok("Saved.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    var commandText = "DELETE FROM \"PC\" WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var affectedRows = command.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        connection.Close();
                        return NotFound();
                    }

                    connection.Close();

                    return Ok("Deleted.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = ex.Message
                });
            }
        }

        [HttpGet("testconn")]
        public IActionResult TestConnection()
        {
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        return Ok("Uspješno spojen na bazu");
                    }
                    else
                    {
                        return StatusCode(500, "Veza s bazom nije uspjela");
                    }

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? name = null, [FromQuery] int? cpu = null, [FromQuery] string? gpu = null)
        {
            var pcs = new List<PC>();
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    var commandText = "SELECT * FROM \"PC\"";
                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var pc = new PC()
                            {
                                Id = Guid.Parse(reader[0].ToString()!),
                                Name = reader[1].ToString()!,
                                CPU = reader[2].ToString()!,
                                GPU = reader[3].ToString()!
                            };
                            pcs.Add(pc);
                        }
                    }
                    else
                    {
                        connection.Close();
                        return NotFound();
                    }

                    connection.Close();

                    return Ok(pcs);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var pc = new PC()
                {   
                    Name = "",
                    CPU = "",
                    GPU = ""
                };

                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    var commandText = "SELECT * FROM \"PC\" WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                  
                        {
                            pc.Id = Guid.Parse(reader[0].ToString()!);
                            pc.Name = reader[1].ToString()!;
                            pc.CPU = reader[2].ToString()!;
                            pc.GPU = reader[3].ToString()!;
                        };
                    }
                    else
                    {
                        connection.Close();
                        return NotFound();
                    }

                    connection.Close();

                    return Ok(pc);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]PC pc)
        {
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseConfig.connString))
                {
                    var commandText = "SELECT \"Id\", \"Name\", \"CPU\", \"GPU\" FROM \"PC\" WHERE \"Id\" = @id;";
                    using var cmd = new NpgsqlCommand(commandText, connection);
                    cmd.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return NotFound();
                    }

                    connection.Close();

                    connection.Open();

                    commandText = "UPDATE \"PC\" set \"Name\" = @name, \"CPU\" = @cpu, \"GPU\" = @gpu WHERE \"Id\" = @id;";
                    using var updateCommand = new NpgsqlCommand(commandText, connection);
                    updateCommand.Parameters.AddWithValue("name", pc.Name);
                    updateCommand.Parameters.AddWithValue("cpu", pc.CPU);
                    updateCommand.Parameters.AddWithValue("gpu", pc.GPU);
                    updateCommand.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    var affectedRows = updateCommand.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        connection.Close();
                        return BadRequest();
                    }

                    connection.Close();

                    return Ok("Updated.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = ex.Message
                });
            }
        }


    }
}
