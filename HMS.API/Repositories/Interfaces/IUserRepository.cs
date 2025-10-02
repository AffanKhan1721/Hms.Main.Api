using HMS.API.Models;

namespace HMS.API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int userId);
    Task AddAsync(User user);
    Task DeleteAsync(User user);
    Task SaveChangesAsync();
}


