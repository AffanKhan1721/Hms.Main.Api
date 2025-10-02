using HMS.API.DTOs.Reservation;
using HMS.API.Mappers;
using HMS.API.Models;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;

namespace HMS.API.Services;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRatesService _ratesService;

    public ReservationService(IUnitOfWork unitOfWork, IRatesService ratesService)
    {
        _unitOfWork = unitOfWork;
        _ratesService = ratesService;
    }

    public async Task<CreateReservationResultDto> CreateReservationAsync(CreateReservationDto dto)
    {
        try
        {
            var checkIn = DateTime.Parse(dto.CheckInDate);
            var checkOut = DateTime.Parse(dto.CheckOutDate);

            if (checkIn >= checkOut)
            {
                return new CreateReservationResultDto { Success = false, ErrorMessage = "Check-in date must be before check-out date." };
            }

            var room = await _unitOfWork.Rooms.GetByIdAsync(dto.RoomRef);
            if (room == null)
            {
                return new CreateReservationResultDto { Success = false, ErrorMessage = "Room not found." };
            }

            var reservation = new Reservation
            {
                CustomerId = dto.UserId,
                RoomId = dto.RoomRef,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                ReservationDate = DateTime.UtcNow,
                Status = "Pending"
            };

            await _unitOfWork.Reservations.AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            var nights = (checkOut - checkIn).Days;
            var price = await _ratesService.GetPriceAsync(room.RoomType, nights);

            return new CreateReservationResultDto
            {
                Success = true,
                ReservationId = reservation.ReservationId,
                Price = price
            };
        }
        catch (FormatException)
        {
            return new CreateReservationResultDto { Success = false, ErrorMessage = "Invalid date format." };
        }
        catch (Exception ex)
        {
            return new CreateReservationResultDto { Success = false, ErrorMessage = $"An error occurred: {ex.Message}" };
        }
    }

    public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId)
    {
        var reservations = await _unitOfWork.Reservations.GetByCustomerIdAsync(userId);
        var reservationDtos = new List<ReservationDto>();

        foreach (var reservation in reservations)
        {
            var nights = (reservation.CheckOutDate - reservation.CheckInDate).Days;
            var price = await _ratesService.GetPriceAsync(reservation.Room?.RoomType ?? "", nights);

            var dto = ReservationMapper.ToDto(reservation);
            dto.Price = price;
            reservationDtos.Add(dto);
        }

        return reservationDtos;
    }

    public async Task<IEnumerable<ReservationManagementDto>> GetPendingReservationsAsync()
    {
        var reservations = await _unitOfWork.Reservations.GetPendingReservationsAsync();
        var reservationDtos = new List<ReservationManagementDto>();

        foreach (var reservation in reservations)
        {
            var nights = (reservation.CheckOutDate - reservation.CheckInDate).Days;
            var price = await _ratesService.GetPriceAsync(reservation.Room?.RoomType ?? "", nights);

            var dto = ReservationMapper.ToManagementDto(reservation);
            dto.Price = price;
            reservationDtos.Add(dto);
        }

        return reservationDtos;
    }

    public async Task<IEnumerable<ReservationManagementDto>> GetAllReservationsAsync()
    {
        var reservations = await _unitOfWork.Reservations.GetAllReservationsAsync();
        var reservationDtos = new List<ReservationManagementDto>();

        foreach (var reservation in reservations)
        {
            var nights = (reservation.CheckOutDate - reservation.CheckInDate).Days;
            var price = await _ratesService.GetPriceAsync(reservation.Room?.RoomType ?? "", nights);

            var dto = ReservationMapper.ToManagementDto(reservation);
            dto.Price = price;
            reservationDtos.Add(dto);
        }

        return reservationDtos;
    }

    public async Task<UpdateReservationStatusResultDto> UpdateReservationStatusAsync(UpdateReservationStatusDto dto)
    {
        try
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(dto.ReservationId);
            if (reservation == null)
            {
                return new UpdateReservationStatusResultDto
                {
                    Success = false,
                    ErrorMessage = "Reservation not found."
                };
            }

            // Validate status
            if (!new[] { "Approved", "Declined", "Pending" }.Contains(dto.Status))
            {
                return new UpdateReservationStatusResultDto
                {
                    Success = false,
                    ErrorMessage = "Invalid status. Must be 'Approved', 'Declined', or 'Pending'."
                };
            }


            var existingReservation = await _unitOfWork.Reservations.GetByIdAsync(dto.ReservationId);
            if (existingReservation == null)
            {
                return new UpdateReservationStatusResultDto
                {
                    Success = false,
                    ErrorMessage = "Reservation not found."
                };
            }


            await _unitOfWork.Reservations.UpdateStatusAsync(dto.ReservationId, dto.Status);


            if (dto.Status == "Approved")
            {
                await _unitOfWork.Rooms.UpdateStatusAsync(existingReservation.RoomId, "Reserved");
            }
            else if (dto.Status == "Declined")
            {
                await _unitOfWork.Rooms.UpdateStatusAsync(existingReservation.RoomId, "Available");
            }

            await _unitOfWork.SaveChangesAsync();

            return new UpdateReservationStatusResultDto { Success = true };
        }
        catch (Exception ex)
        {
            return new UpdateReservationStatusResultDto
            {
                Success = false,
                ErrorMessage = $"An error occurred: {ex.Message}"
            };
        }
    }
}
