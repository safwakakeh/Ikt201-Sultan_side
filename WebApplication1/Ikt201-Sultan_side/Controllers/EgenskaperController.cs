using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using System.Threading.Tasks;

namespace Ikt201_Sultan_side.Controllers
{
    public class EgenskaperController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EgenskaperController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() => View(await _context.Egenskaper.ToListAsync());
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Egenskaper egenskap) { if (ModelState.IsValid) { _context.Add(egenskap); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(egenskap); }
        public async Task<IActionResult> Edit(int id) { var e = await _context.Egenskaper.FindAsync(id); return e == null ? NotFound() : View(e); }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Egenskaper egenskap) { if (id != egenskap.EgenskapId) return NotFound(); if (ModelState.IsValid) { _context.Update(egenskap); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(egenskap); }
        public async Task<IActionResult> Delete(int id) { var e = await _context.Egenskaper.FindAsync(id); return e == null ? NotFound() : View(e); }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) { var e = await _context.Egenskaper.FindAsync(id); if (e != null) { _context.Egenskaper.Remove(e); await _context.SaveChangesAsync(); } return RedirectToAction(nameof(Index)); }
        public async Task<IActionResult> Details(int id) { var e = await _context.Egenskaper.FindAsync(id); return e == null ? NotFound() : View(e); }
    }
}
