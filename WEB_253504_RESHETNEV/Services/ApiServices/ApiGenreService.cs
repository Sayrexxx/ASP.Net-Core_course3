using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WEB_253504_RESHETNEV.Domain.Entities;
using WEB_253504_RESHETNEV.Domain.Models;
using WEB_253504_RESHETNEV.Services.GenreServices;

namespace WEB_253504_RESHETNEV.Services.ApiServices
{
    public class ApiGenreService : IGenreService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiGenreService> _logger;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiGenreService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiGenreService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<ResponseData<List<Genre>>?> GetGenreListAsync()
        {
            var response = await _httpClient.GetAsync(new Uri($"{_httpClient.BaseAddress?.AbsoluteUri}genres"));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<List<Genre>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Ошибка при парсинге JSON: {ex.Message}");
                    return ResponseData<List<Genre>?>.Error($"Ошибка: {ex.Message}");
                }
            }

            _logger.LogError($"Не удалось получить данные от сервера. Ошибка: {response.StatusCode}");
            return ResponseData<List<Genre>?>.Error($"Ошибка сервера: {response.StatusCode}");
        }
    }
}