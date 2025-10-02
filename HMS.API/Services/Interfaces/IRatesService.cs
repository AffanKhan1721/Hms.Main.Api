namespace HMS.API.Services.Interfaces
{
    public interface IRatesService
    {
        Task<decimal> GetPriceAsync(string roomType, int nights);
    }
}
