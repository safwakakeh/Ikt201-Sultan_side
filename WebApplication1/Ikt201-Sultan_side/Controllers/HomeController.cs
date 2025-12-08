using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ikt201_Sultan_side.Models;
using Microsoft.Extensions.Logging;

namespace Ikt201_Sultan_side.Controllers;

public class HomeController : Controller
{
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