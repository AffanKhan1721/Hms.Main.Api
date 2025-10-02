using HMS.API.Constants;
using HMS.API.Data;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;

namespace HMS.API.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly HmsDbContext _context;
    private readonly ICacheService _cacheService;
    private IUserRepository? _users;
    private IRoomRepository? _rooms;
    private IReservationRepository? _reservations;

    public UnitOfWork(HmsDbContext context, ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public IUserRepository Users
    {
        get
        {
            return _users ??= new UserRepository(_context);
        }
    }
    public IRoomRepository Rooms
    {
        get
        {
            return _rooms ??= new RoomRepository(_context);
        }
    }
    public IReservationRepository Reservations
    {
        get
        {
            return _reservations ??= new ReservationRepository(_context);
        }
    }

    public async Task<int> SaveChangesAsync()
    {

        var result = await _context.SaveChangesAsync();

        await _cacheService.RemoveAsync(CacheKeys.Reports.AvailableRooms);
        await _cacheService.RemoveAsync(CacheKeys.Reports.BookedRooms);
        await _cacheService.RemoveAsync(CacheKeys.Reports.Users);

        return result;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}