using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using System.Threading.Tasks;

namespace Ikt201_Sultan_side.Controllers
{
    public class KategoriController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KategoriController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() => View(await _context.Kategorier.ToListAsync());
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Kategori kategori) { if (ModelState.IsValid) { _context.Add(kategori); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(kategori); }
        public async Task<IActionResult> Edit(int id) { var k = await _context.Kategorier.FindAsync(id); return k == null ? NotFound() : View(k); }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Kategori kategori) { if (id != kategori.KategoriId) return NotFound(); if (ModelState.IsValid) { _context.Update(kategori); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(kategori); }
        public async Task<IActionResult> Delete(int id) { var k = await _context.Kategorier.FindAsync(id); return k == null ? NotFound() : View(k); }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) { var k = await _context.Kategorier.FindAsync(id); if (k != null) { _context.Kategorier.Remove(k); await _context.SaveChangesAsync(); } return RedirectToAction(nameof(Index)); }
        public async Task<IActionResult> Details(int id) { var k = await _context.Kategorier.FindAsync(id); return k == null ? NotFound() : View(k); }
    }
}
