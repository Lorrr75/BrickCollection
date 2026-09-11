using System.Text.Json;
using BrickCollection.Models;

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
        public class BricksetSetsResult
        {
            public bool Success { get; init; }
            public List<BricksetSet> Sets { get; init; } = new();
            public int TotalMatches { get; init; }
            public string? ErrorMessage { get; init; }
        }
        
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

        public async Task<BricksetSetsResult> GetOwnedSetsAsync(string apiKey, string userHash)
        {
            var paramsJson = JsonSerializer.Serialize(new { owned = 1, pageSize = 500 });

            var parameters = new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["userHash"] = userHash,
                ["params"] = paramsJson
            };

            using var content = new FormUrlEncodedContent(parameters);
            using var response = await _httpClient.PostAsync($"{BaseUrl}/getSets", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<GetSetsRawResponse>(json, options);
            
            if (result?.status == "success")
            {
                return new BricksetSetsResult
                {
                    Success = true,
                    Sets = result.sets ?? new List<BricksetSet>(),
                    TotalMatches = result.matches
                };
            }
            return new BricksetSetsResult
            {
                Success = false,
               ErrorMessage = result?.message ?? "Errore Sconosciuto"
            };
        }
    }
}