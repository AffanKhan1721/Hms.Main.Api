using HMS.API.DTOs.Room;
using HMS.API.Mappers;
using HMS.API.Models;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;

namespace HMS.API.Services;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await _unitOfWork.Rooms.GetAllAsync();
        return RoomMapper.ToDtoList(rooms);
    }

    public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(string? checkIn, string? checkOut)
    {
        var rooms = await _unitOfWork.Rooms.GetAllAsync();

        var availableRooms = rooms.Where(r => r.Status == "Available");

        return RoomMapper.ToDtoList(availableRooms);
    }

    public async Task<AddRoomResultDto> AddRoomAsync(AddRoomDto dto)
    {
        try
        {
            var existingRooms = await _unitOfWork.Rooms.GetAllAsync();
            if (existingRooms.Any(r => r.RoomNumber == dto.RoomNumber))
            {
                return new AddRoomResultDto
                {
                    Success = false,
                    ErrorMessage = "Room number already exists."
                };
            }

            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                RoomType = dto.RoomType,
                Capacity = dto.Capacity,
                Status = dto.Status
            };

            await _unitOfWork.Rooms.AddAsync(room);
            await _unitOfWork.Rooms.SaveChangesAsync();

            return new AddRoomResultDto
            {
                Success = true,
                RoomId = room.RoomId
            };
        }
        catch (Exception ex)
        {
            return new AddRoomResultDto
            {
                Success = false,
                ErrorMessage = $"An error occurred: {ex.Message}"
            };
        }
    }

    public async Task<DeleteRoomResultDto> DeleteRoomAsync(int roomId)
    {
        try
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
            if (room == null)
            {
                return new DeleteRoomResultDto
                {
                    Success = false,
                    ErrorMessage = "Room not found."
                };
            }


            var reservations = await _unitOfWork.Reservations.GetByCustomerIdAsync(0);


            await _unitOfWork.Rooms.DeleteAsync(room);
            await _unitOfWork.Rooms.SaveChangesAsync();

            return new DeleteRoomResultDto { Success = true };
        }
        catch (Exception ex)
        {
            return new DeleteRoomResultDto
            {
                Success = false,
                ErrorMessage = $"An error occurred: {ex.Message}"
            };
        }
    }
}
