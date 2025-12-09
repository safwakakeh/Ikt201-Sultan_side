using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using System.Threading.Tasks;

namespace Ikt201_Sultan_side.Controllers
{
    public class BordController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BordController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() => View(await _context.Bord.ToListAsync());
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Bord bord) { if (ModelState.IsValid) { _context.Add(bord); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(bord); }
        public async Task<IActionResult> Edit(int id) { var b = await _context.Bord.FindAsync(id); return b == null ? NotFound() : View(b); }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Bord bord) { if (id != bord.BordId) return NotFound(); if (ModelState.IsValid) { _context.Update(bord); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(bord); }
        public async Task<IActionResult> Delete(int id) { var b = await _context.Bord.FindAsync(id); return b == null ? NotFound() : View(b); }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) { var b = await _context.Bord.FindAsync(id); if (b != null) { _context.Bord.Remove(b); await _context.SaveChangesAsync(); } return RedirectToAction(nameof(Index)); }
        public async Task<IActionResult> Details(int id) { var b = await _context.Bord.FindAsync(id); return b == null ? NotFound() : View(b); }
    }
}
