using HMS.API.Services.Interfaces;
using System.Text.Json;

namespace HMS.API.Services
{
    public class RatesService : IRatesService
    {
        private readonly HttpClient _httpClient;

        public RatesService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RatesAPI");
        }

        public async Task<decimal> GetPriceAsync(string roomType, int nights)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/rates?roomType={roomType}&nights={nights}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<PriceResponse>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result?.Price ?? 0m;
                }

                return GetDefaultPrice(roomType, nights);
            }
            catch (Exception)
            {

                return GetDefaultPrice(roomType, nights);
            }
        }

        private static decimal GetDefaultPrice(string roomType, int nights)
        {
            var nightlyRate = roomType.ToLower() switch
            {
                "single" => 50m,
                "double" => 80m,
                "deluxe" => 120m,
                "suite" => 200m,
                _ => 50m
            };

            return nightlyRate * nights;
        }

        private class PriceResponse
        {
            public decimal Price { get; set; }
        }
    }
}
