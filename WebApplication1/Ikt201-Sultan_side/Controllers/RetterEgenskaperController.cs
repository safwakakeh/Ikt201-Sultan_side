using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using System.Threading.Tasks;

namespace Ikt201_Sultan_side.Controllers
{
    public class RetterEgenskaperController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RetterEgenskaperController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() => View(await _context.RetterEgenskaper.ToListAsync());
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(RetterEgenskaper re) { if (ModelState.IsValid) { _context.Add(re); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(re); }
        public async Task<IActionResult> Edit(int id) { var re = await _context.RetterEgenskaper.FindAsync(id); return re == null ? NotFound() : View(re); }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RetterEgenskaper re) { if (id != re.RetterEgenskaperId) return NotFound(); if (ModelState.IsValid) { _context.Update(re); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); } return View(re); }
        public async Task<IActionResult> Delete(int id) { var re = await _context.RetterEgenskaper.FindAsync(id); return re == null ? NotFound() : View(re); }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) { var re = await _context.RetterEgenskaper.FindAsync(id); if (re != null) { _context.RetterEgenskaper.Remove(re); await _context.SaveChangesAsync(); } return RedirectToAction(nameof(Index)); }
        public async Task<IActionResult> Details(int id) { var re = await _context.RetterEgenskaper.FindAsync(id); return re == null ? NotFound() : View(re); }
    }
}
