using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ikt201_Sultan_side.Models;
using Ikt201_Sultan_side.Services;

namespace Ikt201_Sultan_side.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class DishesController : Controller
{
    private readonly IMenuService _menuService;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<DishesController> _logger;

    public DishesController(IMenuService menuService, IWebHostEnvironment environment, ILogger<DishesController> logger)
    {
        _menuService = menuService;
        _environment = environment;
        _logger = logger;
    }

    // GET: Admin/Dishes
    public async Task<IActionResult> Index()
    {
        var dishes = await _menuService.GetAllDishesAsync();
        return View(dishes);
    }

    // GET: Admin/Dishes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Dishes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Dish dish, IFormFile? imageFile)
    {
        if (ModelState.IsValid)
        {
            // Handle image upload
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = await SaveImageAsync(imageFile);
                dish.ImagePath = imagePath;
            }

            var result = await _menuService.CreateDishAsync(dish);
            if (result)
            {
                TempData["SuccessMessage"] = "Rett opprettet!";
                return RedirectToAction(nameof(Index));
            }
            
            ModelState.AddModelError("", "Kunne ikke opprette rett.");
        }
        
        return View(dish);
    }

    // GET: Admin/Dishes/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var dish = await _menuService.GetDishByIdAsync(id);
        if (dish == null)
        {
            return NotFound();
        }
        return View(dish);
    }

    // POST: Admin/Dishes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Dish dish, IFormFile? imageFile)
    {
        if (id != dish.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // Handle new image upload
            if (imageFile != null && imageFile.Length > 0)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(dish.ImagePath))
                {
                    DeleteImage(dish.ImagePath);
                }
                
                var imagePath = await SaveImageAsync(imageFile);
                dish.ImagePath = imagePath;
            }

            var result = await _menuService.UpdateDishAsync(dish);
            if (result)
            {
                TempData["SuccessMessage"] = "Rett oppdatert!";
                return RedirectToAction(nameof(Index));
            }
            
            ModelState.AddModelError("", "Kunne ikke oppdatere rett.");
        }
        
        return View(dish);
    }

    // POST: Admin/Dishes/ToggleAvailability/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleAvailability(int id)
    {
        var result = await _menuService.ToggleAvailabilityAsync(id);
        if (result)
        {
            TempData["SuccessMessage"] = "Tilgjengelighet oppdatert!";
        }
        else
        {
            TempData["ErrorMessage"] = "Kunne ikke oppdatere tilgjengelighet.";
        }
        
        return RedirectToAction(nameof(Index));
    }

    // POST: Admin/Dishes/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var dish = await _menuService.GetDishByIdAsync(id);
        if (dish != null && !string.IsNullOrEmpty(dish.ImagePath))
        {
            DeleteImage(dish.ImagePath);
        }

        var result = await _menuService.DeleteDishAsync(id);
        if (result)
        {
            TempData["SuccessMessage"] = "Rett slettet!";
        }
        else
        {
            TempData["ErrorMessage"] = "Kunne ikke slette rett.";
        }
        
        return RedirectToAction(nameof(Index));
    }

    // Helper methods for image handling
    private async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "dishes");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        return $"/images/dishes/{uniqueFileName}";
    }

    private void DeleteImage(string imagePath)
    {
        try
        {
            var fullPath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting image: {ImagePath}", imagePath);
        }
    }
}