using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ikt201_Sultan_side.Models;
using Microsoft.Extensions.Logging;
using Stripe.Checkout;


namespace Ikt201_Sultan_side.Controllers;

public class HomeController : Controller
{
    public IActionResult BestillingFullfort(string session_id)
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
            TotalAmount = session.AmountTotal ?? 0,
            Currency = session.Currency ?? "nok"
        };

        foreach (var li in lineItems.Data)
        {
            model.Items.Add(new ReceiptItem
            {
                Name = li.Description,
                Quantity = li.Quantity ?? 0, 
                Amount = li.AmountTotal
            });
        }

        return View(model);
    }


    public IActionResult BestillingAvbrutt()
    {
        return View();
    }

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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


