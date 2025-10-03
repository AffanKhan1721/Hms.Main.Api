using HMS.API.DTOs.Reservation;
using HMS.API.Resources;

namespace HMS.API.Services.Interfaces;

public interface IReservationService
{
    Task<CreateReservationResultDto> CreateReservationAsync(CreateReservationDto dto);
    Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId);
    Task<IEnumerable<ReservationManagementDto>> GetPendingReservationsAsync();
    Task<IEnumerable<ReservationManagementDto>> GetAllReservationsAsync();
    Task<UpdateReservationStatusResultDto> UpdateReservationStatusAsync(UpdateReservationStatusDto dto);
    Task<IEnumerable<ReservationResource>> GetUserReservationsResourceAsync(int userId);
    Task<IEnumerable<ReservationManagementResponse>> GetPendingReservationsResourceAsync();
    Task<IEnumerable<ReservationManagementResponse>> GetAllReservationsResourceAsync();
    Task<UpdateReservationStatusResponse> UpdateReservationStatusResourceAsync(UpdateReservationStatusRequest request);
    Task<CreateReservationResource> CreateReservationResourceAsync(int userId, CreateReservationCommand request);
}
