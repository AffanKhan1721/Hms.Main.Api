using HMS.API.DTOs.Reservation;

namespace HMS.API.Services.Interfaces;

public interface IReservationService
{
    Task<CreateReservationResultDto> CreateReservationAsync(CreateReservationDto dto);
    Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId);
    Task<IEnumerable<ReservationManagementDto>> GetPendingReservationsAsync();
    Task<IEnumerable<ReservationManagementDto>> GetAllReservationsAsync();
    Task<UpdateReservationStatusResultDto> UpdateReservationStatusAsync(UpdateReservationStatusDto dto);
}
