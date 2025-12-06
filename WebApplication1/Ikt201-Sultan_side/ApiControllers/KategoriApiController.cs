
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.DTOs;

namespace Ikt201_Sultan_side.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KategoriApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public KategoriApiController(ApplicationDbContext context) { _context = context; }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<KategoriDto>>> GetAll()
        {
            var list = await _context.Kategorier.Select(k => new KategoriDto
            {
                KategoriId = k.KategoriId,
                Navn = k.Navn
            }).ToListAsync();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<KategoriDto>> Get(int id)
        {
            var k = await _context.Kategorier.FindAsync(id);
            if (k == null) return NotFound();
            return Ok(new KategoriDto { KategoriId = k.KategoriId, Navn = k.Navn });
        }


        [HttpPost]
        public async Task<ActionResult<KategoriDto>> Create(KategoriDto kategoriDto)
        {
            if (string.IsNullOrWhiteSpace(kategoriDto.Navn))
                return BadRequest("Navn is required.");
            var kategori = new Kategori { Navn = kategoriDto.Navn };
            _context.Kategorier.Add(kategori);
            await _context.SaveChangesAsync();
            kategoriDto.KategoriId = kategori.KategoriId;
            return CreatedAtAction(nameof(Get), new { id = kategori.KategoriId }, kategoriDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, KategoriDto kategoriDto)
        {
            if (id != kategoriDto.KategoriId) return BadRequest();
            if (string.IsNullOrWhiteSpace(kategoriDto.Navn))
                return BadRequest("Navn is required.");
            var kategori = await _context.Kategorier.FindAsync(id);
            if (kategori == null) return NotFound();
            kategori.Navn = kategoriDto.Navn;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Kategorier.FindAsync(id);
            if (item == null) return NotFound();
            _context.Kategorier.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
