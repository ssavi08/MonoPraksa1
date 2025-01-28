using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCManagement.WebAPI.Models;

namespace PCManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCController : ControllerBase
    {

        public static List<PC> pcs = new List<PC>();
       

        [HttpGet("{id}")]
        public IActionResult GetPCByID(int id)
        {
            var pc = pcs.FirstOrDefault(p => p.Id == id);
            if(pc == null)
            {
                return NotFound();
            }
            return Ok(pc);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PC>> GetAllPCs()
        {
            return Ok(pcs);
        }

        [HttpPost]
        public IActionResult CreatePC([FromBody]PC pc)
        {
            if(pc == null)
            {
                return BadRequest("PC object is null. Please provide valid data.");
            }

            if (string.IsNullOrWhiteSpace(pc.Name) || string.IsNullOrWhiteSpace(pc.Cpu) || string.IsNullOrWhiteSpace(pc.Gpu))
            {
                return BadRequest("Invalid PC data. Name, CPU, and GPU are required.");
            }

            pc.Id = pcs.Any() ? pcs.Max(p => p.Id) +1 : 1;
            pcs.Add(pc);

            return CreatedAtAction(nameof(GetPCByID), new { id = pc.Id}, pc);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePC(int id, [FromBody]PC pc)
        {
            if(pc == null)
            {
                return BadRequest();
            }
            
            var existingPC = pcs.FirstOrDefault(p => p.Id == id);
            if(existingPC == null)
            {
                return NotFound();
            }

            existingPC.Name = pc.Name;
            existingPC.Cpu = pc.Cpu;
            existingPC.Gpu = pc.Gpu;

            return Ok("Update successful");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pc = pcs.FirstOrDefault(pc => pc.Id == id);
            if(pc == null)
            {
                return NotFound();
            }

            pcs.Remove(pc);

            return Ok("Delete successful");
        }
    }
}
