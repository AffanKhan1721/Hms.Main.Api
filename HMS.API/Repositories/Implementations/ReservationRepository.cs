using HMS.API.Data;
using HMS.API.Models;
using HMS.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HMS.API.Repositories.Implementations;

public class ReservationRepository : IReservationRepository
{
    private readonly HmsDbContext _dbContext;

    public ReservationRepository(HmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Reservation>> GetByCustomerIdAsync(int customerId)
    {
        return await _dbContext.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Room)
            .Where(r => r.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Reservation> AddAsync(Reservation reservation)
    {
        await _dbContext.Reservations.AddAsync(reservation);
        return reservation;
    }

    public async Task<IEnumerable<Reservation>> GetPendingReservationsAsync()
    {
        return await _dbContext.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Room)
            .Where(r => r.Status == "Pending")
            .OrderBy(r => r.ReservationDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _dbContext.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Room)
            .OrderByDescending(r => r.ReservationDate)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int reservationId)
    {
        return await _dbContext.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Room)
            .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
    }

    public async Task DeleteAsync(Reservation reservation)
    {
        _dbContext.Reservations.Remove(reservation);
        await Task.CompletedTask;
    }

    public async Task UpdateStatusAsync(int reservationId, string status)
    {
        var reservation = await _dbContext.Reservations.FindAsync(reservationId);
        if (reservation != null)
        {
            reservation.Status = status;
            _dbContext.Reservations.Update(reservation);
        }
        await Task.CompletedTask;
    }
}