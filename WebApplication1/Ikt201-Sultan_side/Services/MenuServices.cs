using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Microsoft.EntityFrameworkCore;

namespace Ikt201_Sultan_side.Services;

public class MenuService : IMenuService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MenuService> _logger;

    public MenuService(ApplicationDbContext context, ILogger<MenuService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Dish>> GetAllDishesAsync()
    {
        return await _context.Dishes.OrderBy(d => d.Category).ThenBy(d => d.Name).ToListAsync();
    }

    public async Task<IEnumerable<Dish>> GetAvailableDishesAsync()
    {
        return await _context.Dishes
            .Where(d => d.IsAvailable)
            .OrderBy(d => d.Category)
            .ThenBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<Dish?> GetDishByIdAsync(int id)
    {
        return await _context.Dishes.FindAsync(id);
    }

    public async Task<bool> CreateDishAsync(Dish dish)
    {
        try
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating dish");
            return false;
        }
    }

    public async Task<bool> UpdateDishAsync(Dish dish)
    {
        try
        {
            dish.UpdatedAt = DateTime.Now;
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating dish");
            return false;
        }
    }

    public async Task<bool> DeleteDishAsync(int id)
    {
        try
        {
            var dish = await GetDishByIdAsync(id);
            if (dish == null) return false;

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting dish");
            return false;
        }
    }

    public async Task<bool> ToggleAvailabilityAsync(int id)
    {
        try
        {
            var dish = await GetDishByIdAsync(id);
            if (dish == null) return false;

            dish.IsAvailable = !dish.IsAvailable;
            dish.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error toggling availability");
            return false;
        }
    }
}