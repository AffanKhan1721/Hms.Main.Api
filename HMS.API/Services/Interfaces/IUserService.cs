using HMS.API.DTOs.User;
using HMS.API.Models;
using HMS.API.Resources;

namespace HMS.API.Services.Interfaces;

public interface IUserService
{
    Task<CreateGuestResultDto> CreateGuestAsync(CreateGuestDto dto);
    Task<AddManagerResultDto> AddManagerAsync(AddManagerDto dto);
    Task<DeleteUserResultDto> DeleteUserAsync(int userId);
    Task<User?> GetByIdAsync(int userId);
    Task<CreateGuestResource> CreateGuestResourceAsync(CreateGuestCommand request);
    Task<AddManagerResponse> AddManagerResourceAsync(AddManagerRequest request);
    Task<DeleteUserResponse> DeleteUserResourceAsync(int userId);
}
