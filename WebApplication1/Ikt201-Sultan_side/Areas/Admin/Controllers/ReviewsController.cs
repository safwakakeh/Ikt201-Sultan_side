using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ikt201_Sultan_side.Services;

namespace Ikt201_Sultan_side.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // GET: Admin/Reviews
    public async Task<IActionResult> Index(bool? pending)
    {
        var reviews = pending == true 
            ? await _reviewService.GetPendingReviewsAsync()
            : await _reviewService.GetAllReviewsAsync();
        
        ViewBag.ShowPending = pending == true;
        return View(reviews);
    }

    // POST: Admin/Reviews/Approve/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        var result = await _reviewService.ApproveReviewAsync(id);
        if (result)
        {
            TempData["SuccessMessage"] = "Tilbakemelding godkjent!";
        }
        else
        {
            TempData["ErrorMessage"] = "Kunne ikke godkjenne tilbakemelding.";
        }
        
        return RedirectToAction(nameof(Index), new { pending = true });
    }

    // POST: Admin/Reviews/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _reviewService.DeleteReviewAsync(id);
        if (result)
        {
            TempData["SuccessMessage"] = "Tilbakemelding slettet!";
        }
        else
        {
            TempData["ErrorMessage"] = "Kunne ikke slette tilbakemelding.";
        }
        
        return RedirectToAction(nameof(Index));
    }
}