using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.DTOs;

namespace Ikt201_Sultan_side.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PersonApiController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            var list = await _context.Personer.Select(p => new PersonDto
            {
                PersonId = p.PersonId,
                Navn = p.Navn,
                Epost = p.Epost,
                Telefon = p.Telefon,
                Admin = p.Admin
            }).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var p = await _context.Personer.FindAsync(id);
            if (p == null) return NotFound();
            return Ok(new PersonDto { PersonId = p.PersonId, Navn = p.Navn, Epost = p.Epost, Telefon = p.Telefon, Admin = p.Admin });
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Create(PersonCreateDto personDto)
        {
            if (string.IsNullOrWhiteSpace(personDto.Navn) || string.IsNullOrWhiteSpace(personDto.Epost))
                return BadRequest("Navn and Epost are required.");

            if (await _context.Personer.AnyAsync(p => p.Epost == personDto.Epost))
                return BadRequest("Email already exists.");
            if (await _context.Personer.AnyAsync(p => p.Telefon == personDto.Telefon))
                return BadRequest("Phone number already exists.");

            var person = new Person { Navn = personDto.Navn, Epost = personDto.Epost, Telefon = personDto.Telefon, Admin = personDto.Admin };
            _context.Personer.Add(person);
            await _context.SaveChangesAsync();
            
            var resultDto = new PersonDto 
            { 
                PersonId = person.PersonId, 
                Navn = person.Navn, 
                Epost = person.Epost, 
                Telefon = person.Telefon, 
                Admin = person.Admin 
            };

            return CreatedAtAction(nameof(Get), new { id = person.PersonId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonDto personDto)
        {
            if (id != personDto.PersonId) return BadRequest();
            if (string.IsNullOrWhiteSpace(personDto.Navn) || string.IsNullOrWhiteSpace(personDto.Epost))
                return BadRequest("Navn and Epost are required.");

            if (await _context.Personer.AnyAsync(p => p.Epost == personDto.Epost && p.PersonId != id))
                return BadRequest("Email already exists.");
            if (await _context.Personer.AnyAsync(p => p.Telefon == personDto.Telefon && p.PersonId != id))
                return BadRequest("Phone number already exists.");

            var person = await _context.Personer.FindAsync(id);
            if (person == null) return NotFound();
            person.Navn = personDto.Navn;
            person.Epost = personDto.Epost;
            person.Telefon = personDto.Telefon;
            person.Admin = personDto.Admin;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Personer.FindAsync(id);
            if (item == null) return NotFound();
            _context.Personer.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
