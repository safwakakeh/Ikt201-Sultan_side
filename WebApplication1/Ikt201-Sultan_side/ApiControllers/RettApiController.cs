
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.DTOs;

namespace Ikt201_Sultan_side.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RettApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RettApiController(ApplicationDbContext context) { _context = context; }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RettDto>>> GetAll()
        {
            var list = await _context.Retter.Select(r => new RettDto
            {
                RettId = r.RettId,
                Navn = r.Navn,
                KategoriId = r.KategoriId,
                Pris = r.Pris,
                Tilgjengelighet = r.Tilgjengelighet,
                Beskrivelse = r.Beskrivelse
            }).ToListAsync();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RettDto>> Get(int id)
        {
            var r = await _context.Retter.FindAsync(id);
            if (r == null) return NotFound();
            return Ok(new RettDto
            {
                RettId = r.RettId,
                Navn = r.Navn,
                KategoriId = r.KategoriId,
                Pris = r.Pris,
                Tilgjengelighet = r.Tilgjengelighet,
                Beskrivelse = r.Beskrivelse
            });
        }


        [HttpPost]
        public async Task<ActionResult<RettDto>> Create(RettCreateDto rettDto)
        {
            if (string.IsNullOrWhiteSpace(rettDto.Navn) || rettDto.KategoriId <= 0)
                return BadRequest("Navn and KategoriId are required.");
            var rett = new Rett
            {
                Navn = rettDto.Navn,
                KategoriId = rettDto.KategoriId,
                Pris = rettDto.Pris,
                Tilgjengelighet = rettDto.Tilgjengelighet,
                Beskrivelse = rettDto.Beskrivelse
            };
            _context.Retter.Add(rett);
            await _context.SaveChangesAsync();
            
            var resultDto = new RettDto
            {
                RettId = rett.RettId,
                Navn = rett.Navn,
                KategoriId = rett.KategoriId,
                Pris = rett.Pris,
                Tilgjengelighet = rett.Tilgjengelighet,
                Beskrivelse = rett.Beskrivelse
            };

            return CreatedAtAction(nameof(Get), new { id = rett.RettId }, resultDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RettDto rettDto)
        {
            if (id != rettDto.RettId) return BadRequest();
            if (string.IsNullOrWhiteSpace(rettDto.Navn) || rettDto.KategoriId <= 0)
                return BadRequest("Navn and KategoriId are required.");
            var rett = await _context.Retter.FindAsync(id);
            if (rett == null) return NotFound();
            rett.Navn = rettDto.Navn;
            rett.KategoriId = rettDto.KategoriId;
            rett.Pris = rettDto.Pris;
            rett.Tilgjengelighet = rettDto.Tilgjengelighet;
            rett.Beskrivelse = rettDto.Beskrivelse;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Retter.FindAsync(id);
            if (item == null) return NotFound();
            _context.Retter.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
