using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe.Checkout;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.Services;
using Microsoft.AspNetCore.Identity;

namespace Ikt201_Sultan_side.Controllers;

public class HomeController : Controller
{
    private readonly EmailService _emailService;
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IMenuService _menuService;
    private readonly IReservationService _reservationService;
    private readonly IReviewService _reviewService;

    public HomeController(
        EmailService emailService, 
        ILogger<HomeController> logger, 
        SignInManager<IdentityUser> signInManager, 
        IMenuService menuService, 
        IReservationService reservationService,
        IReviewService reviewService)
    {
        _emailService = emailService;
        _logger = logger;
        _signInManager = signInManager;
        _menuService = menuService;
        _reservationService = reservationService;
        _reviewService = reviewService;
    }

    public async Task<IActionResult> BestillingFullfort(string session_id)
    {
        if (string.IsNullOrEmpty(session_id))
        {
            return View(new ReceiptViewModel());
        }

        var sessionService = new SessionService();
        var session = sessionService.Get(session_id);

        var lineItems = sessionService.ListLineItems(session_id);

        var model = new ReceiptViewModel
        {
            SessionId = session.Id,
            TotalAmount = session.AmountTotal.GetValueOrDefault(),
            Currency = session.Currency ?? "nok"
        };

        foreach (var li in lineItems.Data)
        {
            model.Items.Add(new ReceiptItem
            {
                Name = li.Description, 
                Quantity = li.Quantity.GetValueOrDefault(),
                Amount = li.AmountTotal
            });
        }

        var customerEmail = session.CustomerDetails?.Email;

        if (!string.IsNullOrEmpty(customerEmail))
        {
            var subject = "Kvittering – Sultan Oslo Food & Sweets";

            var html = $@"
                <h2>Takk for din bestilling!</h2>
                <p>Her er en oppsummering av betalingen:</p>
                <ul>
                    {string.Join("", model.Items.Select(i =>
                        $"<li>{i.Name} – {i.Quantity} stk – {(i.Amount / 100.0M):0.00} NOK</li>"
                    ))}
                </ul>
                <p><strong>Totalbeløp: {(model.TotalAmount / 100.0M):0.00} NOK</strong></p>
                <p>Velkommen igjen!</p>
            ";

            await _emailService.SendEmailAsync(customerEmail, subject, html);
        }

        return View(model);
    }

    public IActionResult BestillingAvbrutt()
    {
        return View();
    }

    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("Admin"))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        return View();
    }

    public async Task<IActionResult> BordBestilling()
    {
        if (User.IsInRole("Admin"))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("BordBestilling");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Bestill(Bordbestilling model)
    {
        if (!ModelState.IsValid)
        {
            return View("BordBestilling", model);
        }

        var reservation = new Reservation
        {
            CustomerName = model.Navn,
            Email = model.Email,
            Phone = model.Tlf,
            ReservationDate = model.Time,
            NumberOfGuests = model.Gjester,
            Status = "Pending",
            CreatedAt = DateTime.Now
        };

        await _reservationService.CreateReservationAsync(reservation);

        TempData["SuccessMessage"] = "Takk for din bestilling! Vi sender en bekreftelse på e-post.";
        return RedirectToAction("BordBestilling");
    }

    public async Task<IActionResult> Meny()
    {
        if (User.IsInRole("Admin"))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Meny");
        }
        var dishes = await _menuService.GetAvailableDishesAsync();
        return View(dishes);
    }

    public async Task<IActionResult> KontaktOss()
    {
        if (User.IsInRole("Admin"))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("KontaktOss");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendTilbakemelding(string Navn, string Email, string Melding)
    {
        if (string.IsNullOrWhiteSpace(Navn) || string.IsNullOrWhiteSpace(Melding))
        {
            ModelState.AddModelError("", "Navn og melding er påkrevd.");
            return View("KontaktOss");
        }

        var review = new Review
        {
            CustomerName = Navn,
            Message = Melding,
            Rating = 5, // Default rating, or add a rating input to the form
            IsApproved = false,
            CreatedAt = DateTime.Now
        };

        await _reviewService.CreateReviewAsync(review);

        TempData["SuccessMessage"] = "Takk for din tilbakemelding! Den vil bli vurdert av oss.";
        return RedirectToAction("KontaktOss");
    }

    public async Task<IActionResult> BestillMat()
    {
        if (User.IsInRole("Admin"))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("BestillMat");
        }
        var dishes = await _menuService.GetAvailableDishesAsync();
        return View(dishes);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
