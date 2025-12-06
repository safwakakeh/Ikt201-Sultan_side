using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.DTOs;

namespace Ikt201_Sultan_side.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetterEgenskaperApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RetterEgenskaperApiController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RetterEgenskaperDto>>> GetAll()
        {
            var list = await _context.RetterEgenskaper.Select(re => new RetterEgenskaperDto
            {
                RetterEgenskaperId = re.RetterEgenskaperId,
                RettId = re.RettId,
                EgenskapId = re.EgenskapId
            }).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RetterEgenskaperDto>> Get(int id)
        {
            var re = await _context.RetterEgenskaper.FindAsync(id);
            if (re == null) return NotFound();
            return Ok(new RetterEgenskaperDto { RetterEgenskaperId = re.RetterEgenskaperId, RettId = re.RettId, EgenskapId = re.EgenskapId });
        }

        [HttpPost]
        public async Task<ActionResult<RetterEgenskaperDto>> Create(RetterEgenskaperCreateDto reDto)
        {
            if (reDto.RettId <= 0 || reDto.EgenskapId <= 0)
                return BadRequest("RettId and EgenskapId must be positive.");
            var re = new RetterEgenskaper { RettId = reDto.RettId, EgenskapId = reDto.EgenskapId };
            _context.RetterEgenskaper.Add(re);
            await _context.SaveChangesAsync();
            
            var resultDto = new RetterEgenskaperDto { RetterEgenskaperId = re.RetterEgenskaperId, RettId = re.RettId, EgenskapId = re.EgenskapId };

            return CreatedAtAction(nameof(Get), new { id = re.RetterEgenskaperId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RetterEgenskaperDto reDto)
        {
            if (id != reDto.RetterEgenskaperId) return BadRequest();
            if (reDto.RettId <= 0 || reDto.EgenskapId <= 0)
                return BadRequest("RettId and EgenskapId must be positive.");
            var re = await _context.RetterEgenskaper.FindAsync(id);
            if (re == null) return NotFound();
            re.RettId = reDto.RettId;
            re.EgenskapId = reDto.EgenskapId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.RetterEgenskaper.FindAsync(id);
            if (item == null) return NotFound();
            _context.RetterEgenskaper.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
