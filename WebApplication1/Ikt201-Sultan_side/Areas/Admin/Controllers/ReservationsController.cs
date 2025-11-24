using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.Services;

namespace Ikt201_Sultan_side.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    // GET: Admin/Reservations
    public async Task<IActionResult> Index(string? status)
    {
        IEnumerable<Reservation> reservations;
        
        if (!string.IsNullOrEmpty(status))
        {
            reservations = await _reservationService.GetReservationsByStatusAsync(status);
            ViewBag.CurrentStatus = status;
        }
        else
        {
            reservations = await _reservationService.GetAllReservationsAsync();
        }
        
        return View(reservations);
    }

    // GET: Admin/Reservations/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }
        return View(reservation);
    }

    // POST: Admin/Reservations/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Reservation reservation)
    {
        if (id != reservation.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var result = await _reservationService.UpdateReservationAsync(reservation);
            if (result)
            {
                TempData["SuccessMessage"] = "Reservasjon oppdatert!";
                return RedirectToAction(nameof(Index));
            }
            
            ModelState.AddModelError("", "Kunne ikke oppdatere reservasjon.");
        }
        
        return View(reservation);
    }

    // POST: Admin/Reservations/UpdateStatus/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        var result = await _reservationService.UpdateStatusAsync(id, status);
        if (result)
        {
            TempData["SuccessMessage"] = $"Status endret til: {status}";
        }
        else
        {
            TempData["ErrorMessage"] = "Kunne ikke oppdatere status.";
        }
        
        return RedirectToAction(nameof(Index));
    }

    // POST: Admin/Reservations/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _reservationService.DeleteReservationAsync(id);
        if (result)
        {
            TempData["SuccessMessage"] = "Reservasjon slettet!";
        }
        else
        {
            TempData["ErrorMessage"] = "Kunne ikke slette reservasjon.";
        }
        
        return RedirectToAction(nameof(Index));
    }
}