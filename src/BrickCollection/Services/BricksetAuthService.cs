using System.Net.Http.Json;
using System.Text.Json;

namespace BrickCollection.Services
{
    public class BricksetAuthResult
    {
        public bool Success { get; set; }
        public string? UserHash { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class BricksetLoginRawResponse
    {
        public string? status { get; set; }
        public string? hash { get; set; }
        public string? message { get; set; }
    }

    public class BricksetAuthService
    {
        private const string BaseUrl = "https://brickset.com/api/v3.asmx";
        private readonly HttpClient _httpClient;

        public BricksetAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BricksetAuthResult> LoginAsync(string apiKey, string username, string password)
        {
            var parameters = new Dictionary<string, string>
            {
                { "apiKey", apiKey },
                { "username", username },
                { "password", password }
            };

            using var content = new FormUrlEncodedContent(parameters);
            using var response = await _httpClient.PostAsync($"{BaseUrl}/login", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BricksetLoginRawResponse>(json);

            if (result?.status == "success")
            {
                return new BricksetAuthResult { Success = true, UserHash = result.hash };
            }

            return new BricksetAuthResult
            { 
                Success = false, 
                ErrorMessage = result?.message ?? "Unknown error"
            };
        }
    }
}