using HMS.API.Constants;
using HMS.API.DTOs.Reports;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;

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

    public async Task<IEnumerable<AvailableRoomReportDto>> GetAvailableRoomsReportAsync()
    {
        const string cacheKey = CacheKeys.Reports.AvailableRooms;


        var cachedResult = await _cacheService.GetAsync<IEnumerable<AvailableRoomReportDto>>(cacheKey);
        if (cachedResult != null)
        {
            Console.WriteLine("GetAvailableRoomsReportAsync: Returning cached data");
            return cachedResult;
        }

        var result = await _reportRepository.GetAllAvailableRoomsAsync();

        await _cacheService.SetAsync(cacheKey, result);

        Console.WriteLine("GetAvailableRoomsReportAsync: Fetched from database and cached");
        return result;
    }

    public async Task<IEnumerable<BookedRoomReportDto>> GetBookedRoomsReportAsync()
    {
        const string cacheKey = CacheKeys.Reports.BookedRooms;

        try
        {

            var cachedResult = await _cacheService.GetAsync<IEnumerable<BookedRoomReportDto>>(cacheKey);
            if (cachedResult != null)
            {
                Console.WriteLine("GetBookedRoomsReportAsync: Returning cached data");
                return cachedResult;
            }

            var result = await _reportRepository.GetAllBookedRoomsAsync();
            Console.WriteLine($"GetBookedRoomsReportAsync: Found {result.Count()} booked rooms");


            await _cacheService.SetAsync(cacheKey, result);

            Console.WriteLine("GetBookedRoomsReportAsync: Fetched from database and cached");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetBookedRoomsReportAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<UserReportDto>> GetUsersReportAsync()
    {
        const string cacheKey = CacheKeys.Reports.Users;
        var cachedResult = await _cacheService.GetAsync<IEnumerable<UserReportDto>>(cacheKey);
        if (cachedResult != null)
        {
            Console.WriteLine("GetUsersReportAsync: Returning cached data");
            return cachedResult;
        }


        var result = await _reportRepository.GetAllUsersAsync();


        await _cacheService.SetAsync(cacheKey, result);

        Console.WriteLine("GetUsersReportAsync: Fetched from database and cached");
        return result;
    }



}
