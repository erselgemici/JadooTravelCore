namespace JadooTravel.Services.TravelAiServices
{
    public interface ITravelAiService
    {
        Task<List<string>> GetPlacesAsync(string city, string country);
    }
}
