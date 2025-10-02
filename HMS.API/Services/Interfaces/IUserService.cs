using HMS.API.DTOs.User;
using HMS.API.Models;

namespace HMS.API.Services.Interfaces;

public interface IUserService
{
    Task<CreateGuestResultDto> CreateGuestAsync(CreateGuestDto dto);
    Task<AddManagerResultDto> AddManagerAsync(AddManagerDto dto);
    Task<DeleteUserResultDto> DeleteUserAsync(int userId);
    Task<User?> GetByIdAsync(int userId);
}
