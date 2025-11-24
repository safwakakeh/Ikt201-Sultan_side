using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Microsoft.EntityFrameworkCore;

namespace Ikt201_Sultan_side.Services;

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ReservationService> _logger;

    public ReservationService(ApplicationDbContext context, ILogger<ReservationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _context.Reservations
            .OrderByDescending(r => r.ReservationDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByStatusAsync(string status)
    {
        return await _context.Reservations
            .Where(r => r.Status == status)
            .OrderByDescending(r => r.ReservationDate)
            .ToListAsync();
    }

    public async Task<Reservation?> GetReservationByIdAsync(int id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task<bool> CreateReservationAsync(Reservation reservation)
    {
        try
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating reservation");
            return false;
        }
    }

    public async Task<bool> UpdateReservationAsync(Reservation reservation)
    {
        try
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating reservation");
            return false;
        }
    }
    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        try
        {
            var reservation = await GetReservationByIdAsync(id);
            if (reservation == null) return false;
            
            reservation.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating status");
            return false;
        }
    }

    public async Task<bool> DeleteReservationAsync(int id)
    {
        try
        {
            var reservation = await GetReservationByIdAsync(id);
            if (reservation == null) return false;
            
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting reservation");
            return false;
        }
    }
}
