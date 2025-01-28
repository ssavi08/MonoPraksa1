using Microsoft.AspNetCore.Mvc;
using PCManagement.Common;
using PCManagement.ServiceCommon;
using PCManagement.WebAPI.Models;

namespace PCManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCController : ControllerBase
    {
        private IPCService _service;

        public PCController(IPCService servo)
        {
            _service = servo;
        }

        [HttpPost]
        public async Task<IActionResult> AddPCAsync([FromBody] PC pc)
        {
            if (pc == null)
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = "Invalid data."
                });

            //PCService pcService = new PCService();

            bool result = await _service.AddPCAsync(pc);

            if (result)
            {
                return Ok("PC added.");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePCAsync(Guid id)
        {
            var result = await _service.DeletePCAsync(id);
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
        public IActionResult TestDatabaseConnectionAsync()
        {
            //var result = ;
            if (_service.TestDatabaseConnectionAsync())
            {
                return Ok("Successfully connected to the database.");
            }

            return StatusCode(500, "Failed to connect to the database.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPCsAsync([FromQuery] string orderBy = "Id",
                [FromQuery] string sortOrder = "ASC",
                [FromQuery] string searchQuery = "",
                [FromQuery] string cpu = "",
                [FromQuery] string gpu = "")
        {
            //PCService pcService = new PCService();
            var filter = new PCFilter
            {
                SearchQuery = searchQuery
            };

            var sorting = new Sorting
            {
                OrderBy = orderBy,
                SortOrder = sortOrder
            };

            var pcs = await _service.GetAllPCsAsync(sorting, filter);

            if (pcs.Count > 0)
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
        public async Task<IActionResult> GetPCAsync(Guid id)
        {
            var pc = await _service.GetPCAsync(id);
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
        public async Task<IActionResult> UpdatePCAsync(Guid id, [FromBody] PC updatedPC)
        {
            if (updatedPC == null)
            {
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = "Invalid data."
                });
            }

            bool result = await _service.UpdatePCAsync(id, updatedPC);

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