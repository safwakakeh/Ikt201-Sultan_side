
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.DTOs;

namespace Ikt201_Sultan_side.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BookingApiController(ApplicationDbContext context) { _context = context; }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
        {
            return await _context.Bookinger.Select(b => new BookingDto
            {
                BookingId = b.BookingId,
                KundeId = b.PersonId,
                BordId = b.BordId,
                Tid = b.Tid,
                TidSlutt = b.TidSlutt,
                AntallGjester = b.AntallGjester,
                Bekreftet = b.Bekreftet,
                BekreftetAdminId = b.BekreftetAdminId,
                BekreftetTid = b.BekreftetTid
            }).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> Get(int id)
        {
            var b = await _context.Bookinger.FindAsync(id);
            if (b == null) return NotFound();
            return Ok(new BookingDto
            {
                BookingId = b.BookingId,
                KundeId = b.PersonId,
                BordId = b.BordId,
                Tid = b.Tid,
                TidSlutt = b.TidSlutt,
                AntallGjester = b.AntallGjester,
                Bekreftet = b.Bekreftet,
                BekreftetAdminId = b.BekreftetAdminId,
                BekreftetTid = b.BekreftetTid
            });
        }


        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create(BookingDto bookingDto)
        {
            if (bookingDto.KundeId <= 0 || bookingDto.BordId <= 0 || bookingDto.AntallGjester <= 0)
                return BadRequest("KundeId, BordId, and AntallGjester must be positive.");

            if (!await _context.Personer.AnyAsync(p => p.PersonId == bookingDto.KundeId))
                return BadRequest($"Person with ID {bookingDto.KundeId} does not exist.");

            if (!await _context.Bord.AnyAsync(b => b.BordId == bookingDto.BordId))
                return BadRequest($"Bord with ID {bookingDto.BordId} does not exist.");

            if (bookingDto.BekreftetAdminId.HasValue && !await _context.Personer.AnyAsync(p => p.PersonId == bookingDto.BekreftetAdminId.Value))
                return BadRequest($"Admin Person with ID {bookingDto.BekreftetAdminId.Value} does not exist.");

            try
            {
                var booking = new Booking
                {
                    PersonId = bookingDto.KundeId,
                    BordId = bookingDto.BordId,
                    Tid = bookingDto.Tid,
                    TidSlutt = bookingDto.TidSlutt,
                    AntallGjester = bookingDto.AntallGjester,
                    Bekreftet = bookingDto.Bekreftet,
                    BekreftetAdminId = bookingDto.BekreftetAdminId,
                    BekreftetTid = bookingDto.BekreftetTid
                };
                _context.Bookinger.Add(booking);
                await _context.SaveChangesAsync();
                bookingDto.BookingId = booking.BookingId;
                return CreatedAtAction(nameof(Get), new { id = booking.BookingId }, bookingDto);
            }
            catch (Exception ex)
            {
                // Log the error to the response for debugging
                var innerMessage = ex.InnerException?.Message ?? "No inner exception";
                return StatusCode(500, $"Internal server error: {ex.Message}\nInner Exception: {innerMessage}\n{ex.StackTrace}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookingDto bookingDto)
        {
            if (id != bookingDto.BookingId) return BadRequest();
            if (bookingDto.KundeId <= 0 || bookingDto.BordId <= 0 || bookingDto.AntallGjester <= 0)
                return BadRequest("KundeId, BordId, and AntallGjester must be positive.");

            if (!await _context.Personer.AnyAsync(p => p.PersonId == bookingDto.KundeId))
                return BadRequest($"Person with ID {bookingDto.KundeId} does not exist.");

            if (!await _context.Bord.AnyAsync(b => b.BordId == bookingDto.BordId))
                return BadRequest($"Bord with ID {bookingDto.BordId} does not exist.");

            var booking = await _context.Bookinger.FindAsync(id);
            if (booking == null) return NotFound();
            booking.PersonId = bookingDto.KundeId;
            booking.BordId = bookingDto.BordId;
            booking.Tid = bookingDto.Tid;
            booking.TidSlutt = bookingDto.TidSlutt;
            booking.AntallGjester = bookingDto.AntallGjester;
            booking.Bekreftet = bookingDto.Bekreftet;
            booking.BekreftetAdminId = bookingDto.BekreftetAdminId;
            booking.BekreftetTid = bookingDto.BekreftetTid;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Bookinger.FindAsync(id);
            if (item == null) return NotFound();
            _context.Bookinger.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
