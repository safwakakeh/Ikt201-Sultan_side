using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using System.Threading.Tasks;

namespace Ikt201_Sultan_side.Controllers
{
    public class RettController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RettController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() => View(await _context.Retter.ToListAsync());
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Rett rett) { if (ModelState.IsValid) { _context.Add(rett); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(rett); }
        public async Task<IActionResult> Edit(int id) { var r = await _context.Retter.FindAsync(id); return r == null ? NotFound() : View(r); }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Rett rett) { if (id != rett.RettId) return NotFound(); if (ModelState.IsValid) { _context.Update(rett); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(rett); }
        public async Task<IActionResult> Delete(int id) { var r = await _context.Retter.FindAsync(id); return r == null ? NotFound() : View(r); }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) { var r = await _context.Retter.FindAsync(id); if (r != null) { _context.Retter.Remove(r); await _context.SaveChangesAsync(); } return RedirectToAction(nameof(Index)); }
        public async Task<IActionResult> Details(int id) { var r = await _context.Retter.FindAsync(id); return r == null ? NotFound() : View(r); }
    }
}
