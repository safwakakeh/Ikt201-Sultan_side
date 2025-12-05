using Microsoft.AspNetCore.Mvc;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;

namespace Ikt201_Sultan_side.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Register person
        [HttpPost]
        public IActionResult RegisterPerson([FromBody] Person person)
        {
            _context.Personer.Add(person);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPerson), new { id = person.PersonId }, person);
        }

        // Read person
        [HttpGet("{id}")]
        public IActionResult GetPerson(int id)
        {
            var person = _context.Personer.Find(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // Update person
        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
        {
            var person = _context.Personer.Find(id);
            if (person == null) return NotFound();

            person.Navn = updatedPerson.Navn;
            person.Epost = updatedPerson.Epost;
            person.Telefon = updatedPerson.Telefon;
            person.Admin = updatedPerson.Admin;

            _context.SaveChanges();
            return NoContent();
        }

        // Delete person
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var person = _context.Personer.Find(id);
            if (person == null) return NotFound();

            _context.Personer.Remove(person);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
