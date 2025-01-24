using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using PCManagement.Common;
using PCManagement.Service;
using PCManagement.WebAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PCManagement.WebAPI.Controllers
{
   

    public class NpgsqlPCManagementRepository
    {
        private readonly NpgsqlConnection _connection;

        public NpgsqlPCManagementRepository()
        {
            _connection = new NpgsqlConnection(DatabaseConfig.connString);
            _connection.Open();
        }

        

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

            PCService pcService = new PCService();

            bool result = pcService.AddPc(pc);

            if(result)
            {
                return Ok();
            }
            return BadRequest();
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            PCService pcService = new PCService();
            bool result = pcService.DeletePC(id);
            if (result)
            {
                return Ok("Deleted.");
            }

            return NotFound(new
            {
                error = "Not Found",
                message = "PC with the specified ID does not exist or update failed."
            });

        }

        [HttpGet("testconn")]
        public IActionResult TestConnection()
        {
            PCService pcService = new PCService();

            var result = pcService.TestConnection();
            if (result)
            {
                return Ok("Successfully connected to the database.");
            }

            return StatusCode(500, "Failed to connect to the database.");
        }

        /*DODATI FILTERE*/
        [HttpGet]
        public IActionResult GetAll()
        {
            PCService pcService = new PCService();
            var pcs = pcService.GetAll();

            if(pcs.Count > 0)
            {
                return Ok(pcs);
            }

            return NotFound(new
            {
                error = "Not Found",
                message = "No PCs found."
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            PCService pcService = new PCService();

            var pc = pcService.GetPc(id);
            if (pc != null)
            {
                return Ok(pc);
            }

            return NotFound(new
            {
                error = "Not Found",
                message = "That PC not exist."
            });
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]PC updatedPC)
        {
            if (updatedPC == null)
            {
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = "Invalid data."
                });
            }

            PCService pcService = new PCService();
            bool result = pcService.UpdatePC(id, updatedPC);

            if (result)
            {
                return Ok("Updated.");
            }

            return NotFound(new
            {
                error = "Not Found",
                message = "PC with the specified ID does not exist or update failed."
            });
        }


    }
}
