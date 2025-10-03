using HMS.API.Data;
using HMS.API.Models;
using HMS.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HMS.API.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly HmsDbContext _dbContext;

    public UserRepository(HmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        return _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
