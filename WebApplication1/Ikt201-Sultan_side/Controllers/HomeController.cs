using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe.Checkout;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.Services;

namespace Ikt201_Sultan_side.Controllers;

public class HomeController : Controller
{
    private readonly EmailService _emailService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(EmailService emailService, ILogger<HomeController> logger)
    {
        _emailService = emailService;
        _logger = logger;
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

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult BordBestilling()
    {
        return View();
    }

    public IActionResult Meny()
    {
        return View();
    }

    public IActionResult KontaktOss()
    {
        return View();
    }

    public IActionResult BestillMat()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}