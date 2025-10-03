using HMS.API.Models;

namespace HMS.API.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetByCustomerIdAsync(int customerId);
    Task<IEnumerable<Reservation>> GetPendingReservationsAsync();
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<Reservation?> GetByIdAsync(int reservationId);
    Task<Reservation> AddAsync(Reservation reservation);
    Task DeleteAsync(Reservation reservation);
    Task UpdateStatusAsync(int reservationId, string status);
}
