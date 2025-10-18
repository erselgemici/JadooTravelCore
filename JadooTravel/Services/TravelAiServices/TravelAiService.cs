using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JadooTravel.Services.TravelAiServices
{
    public class TravelAiService : ITravelAiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public TravelAiService(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<List<string>> GetPlacesAsync(string city, string country)
        {
            var apiKey = _config["Gemini:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("Gemini API anahtarı bulunamadı.");

            // 🔹 Türkçe gezi önerisi isteği
            var prompt = $@"
Sen bir profesyonel seyahat rehberisin. 
{city}, {country} için gezilecek en az 8 önemli yeri listele. 
Her biri için:
1. Yer adı (kalın yaz).
2. Kısa tanımı (2-3 cümle).
3. Neden popüler olduğu (örneğin tarihî, kültürel, doğal güzellik vb.).
4. Yakınında görülebilecek bir ekstra tavsiye (örneğin bir kafe, park, müze).
Cevabı madde madde, Türkçe olarak ve Markdown formatında ver.
";

            var body = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            // ✅ En güncel model (senin hesabında aktif olan)
            var response = await client.PostAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}",
                content);

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception("Gemini API hatası: " + result);

            using var doc = JsonDocument.Parse(result);

            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text.Split('\n', StringSplitOptions.RemoveEmptyEntries)
           .Select(p => p
               .Replace("True ?", "", StringComparison.OrdinalIgnoreCase)
               .Replace("\"", "")
               .Replace(":", "")
               .Replace("*", "")
               .Trim('-', '•', ' ', '\r', '\n'))
           .Where(p => !string.IsNullOrWhiteSpace(p))
           .ToList();
        }

    }
}
