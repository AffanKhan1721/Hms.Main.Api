using HMS.API.Models;

namespace HMS.API.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRoomRepository Rooms { get; }
    IReservationRepository Reservations { get; }
    Task<int> SaveChangesAsync();
}