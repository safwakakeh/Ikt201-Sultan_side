using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using System.Threading.Tasks;

namespace Ikt201_Sultan_side.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookingController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() => View(await _context.Bookinger.ToListAsync());
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Booking booking) { if (ModelState.IsValid) { _context.Add(booking); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(booking); }
        public async Task<IActionResult> Edit(int id) { var b = await _context.Bookinger.FindAsync(id); return b == null ? NotFound() : View(b); }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Booking booking) { if (id != booking.BookingId) return NotFound(); if (ModelState.IsValid) { _context.Update(booking); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(booking); }
        public async Task<IActionResult> Delete(int id) { var b = await _context.Bookinger.FindAsync(id); return b == null ? NotFound() : View(b); }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) { var b = await _context.Bookinger.FindAsync(id); if (b != null) { _context.Bookinger.Remove(b); await _context.SaveChangesAsync(); } return RedirectToAction(nameof(Index)); }
        public async Task<IActionResult> Details(int id) { var b = await _context.Bookinger.FindAsync(id); return b == null ? NotFound() : View(b); }
    }
}
