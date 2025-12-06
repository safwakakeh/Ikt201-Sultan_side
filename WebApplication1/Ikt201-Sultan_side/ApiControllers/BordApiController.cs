
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.DTOs;

namespace Ikt201_Sultan_side.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BordApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BordApiController(ApplicationDbContext context) { _context = context; }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BordDto>>> GetAll()
        {
            var list = await _context.Bord.Select(b => new BordDto
            {
                BordId = b.BordId,
                Plasser = b.Plasser,
                MaksPlasser = b.MaksPlasser
            }).ToListAsync();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BordDto>> Get(int id)
        {
            var b = await _context.Bord.FindAsync(id);
            if (b == null) return NotFound();
            return Ok(new BordDto { BordId = b.BordId, Plasser = b.Plasser, MaksPlasser = b.MaksPlasser });
        }


        [HttpPost]
        public async Task<ActionResult<BordDto>> Create(BordDto bordDto)
        {
            if (bordDto.Plasser <= 0 || bordDto.MaksPlasser <= 0)
                return BadRequest("Plasser and MaksPlasser must be positive.");
            var bord = new Bord { Plasser = bordDto.Plasser, MaksPlasser = bordDto.MaksPlasser };
            _context.Bord.Add(bord);
            await _context.SaveChangesAsync();
            bordDto.BordId = bord.BordId;
            return CreatedAtAction(nameof(Get), new { id = bord.BordId }, bordDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BordDto bordDto)
        {
            if (id != bordDto.BordId) return BadRequest();
            if (bordDto.Plasser <= 0 || bordDto.MaksPlasser <= 0)
                return BadRequest("Plasser and MaksPlasser must be positive.");
            var bord = await _context.Bord.FindAsync(id);
            if (bord == null) return NotFound();
            bord.Plasser = bordDto.Plasser;
            bord.MaksPlasser = bordDto.MaksPlasser;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Bord.FindAsync(id);
            if (item == null) return NotFound();
            _context.Bord.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
