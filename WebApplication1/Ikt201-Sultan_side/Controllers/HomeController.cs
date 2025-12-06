using System.Diagnostics;
using Ikt201_Sultan_side.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ikt201_Sultan_side.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;

    public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _signInManager = signInManager;
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

    public async Task<IActionResult> Meny()
    {
        if (User.IsInRole("Admin"))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Meny");
        }
        return View();
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

    public async Task<IActionResult> OmOss()
    {
        if (User.IsInRole("Admin"))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("OmOss");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
