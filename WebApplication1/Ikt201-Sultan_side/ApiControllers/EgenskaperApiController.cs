
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.DTOs;

namespace Ikt201_Sultan_side.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EgenskaperApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EgenskaperApiController(ApplicationDbContext context) { _context = context; }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EgenskaperDto>>> GetAll()
        {
            var list = await _context.Egenskaper.Select(e => new EgenskaperDto
            {
                EgenskapId = e.EgenskapId,
                Navn = e.Navn
            }).ToListAsync();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EgenskaperDto>> Get(int id)
        {
            var item = await _context.Egenskaper.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(new EgenskaperDto { EgenskapId = item.EgenskapId, Navn = item.Navn });
        }


        [HttpPost]
        public async Task<ActionResult<EgenskaperDto>> Create(EgenskaperDto egenskapDto)
        {
            if (string.IsNullOrWhiteSpace(egenskapDto.Navn))
                return BadRequest("Navn is required.");
            var egenskap = new Egenskaper { Navn = egenskapDto.Navn };
            _context.Egenskaper.Add(egenskap);
            await _context.SaveChangesAsync();
            egenskapDto.EgenskapId = egenskap.EgenskapId;
            return CreatedAtAction(nameof(Get), new { id = egenskap.EgenskapId }, egenskapDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EgenskaperDto egenskapDto)
        {
            if (id != egenskapDto.EgenskapId) return BadRequest();
            if (string.IsNullOrWhiteSpace(egenskapDto.Navn))
                return BadRequest("Navn is required.");
            var egenskap = await _context.Egenskaper.FindAsync(id);
            if (egenskap == null) return NotFound();
            egenskap.Navn = egenskapDto.Navn;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Egenskaper.FindAsync(id);
            if (item == null) return NotFound();
            _context.Egenskaper.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
