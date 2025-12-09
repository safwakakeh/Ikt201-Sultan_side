using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ikt201_Sultan_side.Data;
using Microsoft.EntityFrameworkCore;

namespace Ikt201_Sultan_side.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize] // Kun innloggede brukere
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Dashboard statistikk
        ViewBag.TotalDishes = await _context.Dishes.CountAsync();
        ViewBag.AvailableDishes = await _context.Dishes.CountAsync(d => d.IsAvailable);
        
        ViewBag.TotalReservations = await _context.Reservations.CountAsync();
        ViewBag.PendingReservations = await _context.Reservations.CountAsync(r => r.Status == "Pending");
        ViewBag.TodaysReservations = await _context.Reservations
            .CountAsync(r => r.ReservationDate.Date == DateTime.Today);
        
        ViewBag.TotalReviews = await _context.Reviews.CountAsync();
        ViewBag.PendingReviews = await _context.Reviews.CountAsync(r => !r.IsApproved);
        
        ViewBag.TotalOrders = await _context.Orders.CountAsync();
        ViewBag.TodaysOrders = await _context.Orders.CountAsync(o => o.CreatedAt.Date == DateTime.Today);
        
        // Siste reservasjoner
        ViewBag.RecentReservations = await _context.Reservations
            .OrderByDescending(r => r.CreatedAt)
            .Take(5)
            .ToListAsync();
        
        return View();
    }
}