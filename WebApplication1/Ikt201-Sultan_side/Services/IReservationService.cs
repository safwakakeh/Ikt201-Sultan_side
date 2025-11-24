using Ikt201_Sultan_side.Models;

namespace Ikt201_Sultan_side.Services;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<IEnumerable<Reservation>> GetReservationsByStatusAsync(string status);
    Task<Reservation?> GetReservationByIdAsync(int id);
    Task<bool> CreateReservationAsync(Reservation reservation);
    Task<bool> UpdateReservationAsync(Reservation reservation);
    Task<bool> UpdateStatusAsync(int id, string status);
    Task<bool> DeleteReservationAsync(int id);
}