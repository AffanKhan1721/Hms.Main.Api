using HMS.API.DTOs.User;
using HMS.API.Models;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;
using HMS.API.Resources;
using HMS.API.Mappers;

namespace HMS.API.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHasher _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, PasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreateGuestResultDto> CreateGuestAsync(CreateGuestDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
        {
            return new CreateGuestResultDto { Success = false, ErrorMessage = "Name, email, and password are required." };
        }

        var existingUser = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return new CreateGuestResultDto { Success = false, ErrorMessage = "User with this email already exists." };
        }

        var passwordHash = _passwordHasher.HashPassword(dto.Password);

        var user = new User
        {
            FullName = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
            PhoneNumber = dto.Phone ?? string.Empty,
            Role = "Guest"
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new CreateGuestResultDto { Success = true };
    }

    public async Task<CreateGuestResource> CreateGuestResourceAsync(CreateGuestCommand request)
    {
        var dto = ResourceMapper.ToCreateGuestDto(request);
        var result = await CreateGuestAsync(dto);
        return ResourceMapper.ToCreateGuestResponse(result);
    }

    public async Task<AddManagerResultDto> AddManagerAsync(AddManagerDto dto)
    {
        if (string.IsNullOrEmpty(dto.FullName) || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
        {
            return new AddManagerResultDto { Success = false, ErrorMessage = "Full name, email, and password are required." };
        }
        
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return new AddManagerResultDto { Success = false, ErrorMessage = "User with this email already exists." };
        }

        var passwordHash = _passwordHasher.HashPassword(dto.Password);

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = passwordHash,
            PhoneNumber = dto.PhoneNumber ?? string.Empty,
            Role = "Manager"
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.Users.SaveChangesAsync();

        return new AddManagerResultDto { Success = true, UserId = user.UserId };
    }

    public async Task<AddManagerResponse> AddManagerResourceAsync(AddManagerRequest request)
    {
        var dto = ResourceMapper.ToAddManagerDto(request);
        var result = await AddManagerAsync(dto);
        return ResourceMapper.ToAddManagerResponse(result);
    }

    public async Task<DeleteUserResultDto> DeleteUserAsync(int userId)
    {
        try
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return new DeleteUserResultDto { Success = false, ErrorMessage = "User not found." };
            }

            if (user.Role == "Admin")
            {
                return new DeleteUserResultDto { Success = false, ErrorMessage = "Cannot delete admin users." };
            }
            var userReservations = await _unitOfWork.Reservations.GetByCustomerIdAsync(userId);
            foreach (var reservation in userReservations)
            {
                await _unitOfWork.Reservations.DeleteAsync(reservation);
            }


            await _unitOfWork.Users.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new DeleteUserResultDto { Success = true };
        }
        catch (Exception ex)
        {
            return new DeleteUserResultDto { Success = false, ErrorMessage = $"An error occurred: {ex.Message}" };
        }
    }

    public async Task<DeleteUserResponse> DeleteUserResourceAsync(int userId)
    {
        var result = await DeleteUserAsync(userId);
        return ResourceMapper.ToDeleteUserResponse(result);
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _unitOfWork.Users.GetByIdAsync(userId);
    }
}
