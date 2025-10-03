using HMS.API.Constants;
using HMS.API.DTOs.Reports;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;
using HMS.API.Mappers;

namespace HMS.API.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly ICacheService _cacheService;

    public ReportService(IReportRepository reportRepository, ICacheService cacheService)
    {
        _reportRepository = reportRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Resources.AvailableRoomReportResponse>> GetAvailableRoomsReportAsync()
    {
        const string cacheKey = CacheKeys.Reports.AvailableRooms;


        var cachedResult = await _cacheService.GetAsync<IEnumerable<Resources.AvailableRoomReportResponse>>(cacheKey);
        if (cachedResult != null)
        {
            Console.WriteLine("GetAvailableRoomsReportAsync: Returning cached data");
            return cachedResult;
        }

        var result = await _reportRepository.GetAllAvailableRoomsAsync();

        var reportResponses = ReportMapper.ToAvailableRoomReportResponseList(result);
        await _cacheService.SetAsync(cacheKey, reportResponses);

        Console.WriteLine("GetAvailableRoomsReportAsync: Fetched from database and cached");
        return reportResponses;
    }

    public async Task<IEnumerable<Resources.BookedRoomReportResponse>> GetBookedRoomsReportAsync()
    {
        const string cacheKey = CacheKeys.Reports.BookedRooms;

        try
        {

            var cachedResult = await _cacheService.GetAsync<IEnumerable<Resources.BookedRoomReportResponse>>(cacheKey);
            if (cachedResult != null)
            {
                Console.WriteLine("GetBookedRoomsReportAsync: Returning cached data");
                return cachedResult;
            }

            var result = await _reportRepository.GetAllBookedRoomsAsync();
            Console.WriteLine($"GetBookedRoomsReportAsync: Found {result.Count()} booked rooms");

            var reportResponses = ReportMapper.ToBookedRoomReportResponseList(result);
            await _cacheService.SetAsync(cacheKey, reportResponses);

            Console.WriteLine("GetBookedRoomsReportAsync: Fetched from database and cached");
            return reportResponses;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetBookedRoomsReportAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Resources.UserReportResponse>> GetUsersReportAsync()
    {
        const string cacheKey = CacheKeys.Reports.Users;
        var cachedResult = await _cacheService.GetAsync<IEnumerable<Resources.UserReportResponse>>(cacheKey);
        if (cachedResult != null)
        {
            Console.WriteLine("GetUsersReportAsync: Returning cached data");
            return cachedResult;
        }


        var result = await _reportRepository.GetAllUsersAsync();

        var reportResponses = ReportMapper.ToUserReportResponseList(result);
        await _cacheService.SetAsync(cacheKey, reportResponses);

        Console.WriteLine("GetUsersReportAsync: Fetched from database and cached");
        return reportResponses;
    }



}
