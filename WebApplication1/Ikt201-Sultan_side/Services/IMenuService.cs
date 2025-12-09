using Ikt201_Sultan_side.Models;

namespace Ikt201_Sultan_side.Services;

public interface IMenuService
{
    Task<IEnumerable<Dish>> GetAllDishesAsync();
    Task<IEnumerable<Dish>> GetAvailableDishesAsync();
    Task<Dish?> GetDishByIdAsync(int id);
    Task<bool> CreateDishAsync(Dish dish);
    Task<bool> UpdateDishAsync(Dish dish);
    Task<bool> DeleteDishAsync(int id);
    Task<bool> ToggleAvailabilityAsync(int id);
}