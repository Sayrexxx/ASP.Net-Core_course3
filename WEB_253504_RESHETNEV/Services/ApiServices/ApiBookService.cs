using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WEB_253504_RESHETNEV.Domain.Entities;
using WEB_253504_RESHETNEV.Domain.Models;
using WEB_253504_RESHETNEV.Services.BookServices;

namespace WEB_253504_RESHETNEV.Services.ApiServices
{
    public class ApiBookService : IBookService
    {
        private readonly HttpClient _httpClient;
        private readonly string _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ILogger<ApiBookService> _logger;
        private readonly IConfiguration _configuration;

        public ApiBookService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiBookService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _pageSize = configuration.GetSection("ItemsPerPage").Value ?? "3";
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public async Task<ResponseData<ProductListModel<Book>>?> GetBookListAsync(string? genreName, int pageNo = 1)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress}books/");

            if (genreName != null)
            {
                urlString.Append($"?genreName={genreName}");
            }

            _logger.LogInformation($"Запрашиваемый URI: {urlString}");
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var itemsPerPage = Convert.ToInt32(_configuration.GetRequiredSection("ItemsPerPage").Value);
                    var resp = await response.Content.ReadFromJsonAsync<List<Book>>(
                        _serializerOptions);
                    _logger.LogInformation($"Ответ сервера: {resp}");

                    var filteredBooks = resp;
                    var totalBooks = filteredBooks.Count;
                    var totalPages = (int)Math.Ceiling(totalBooks / Double.Parse(_pageSize));
                    var booksOnPage = filteredBooks
                        .Skip((pageNo - 1) * itemsPerPage)
                        .Take(itemsPerPage)
                        .ToList();

                    var productListModel = new ProductListModel<Book>()
                    {
                        Items = booksOnPage,
                        CurrentPage = pageNo,
                        TotalPages = totalPages
                    };
                    return ResponseData<ProductListModel<Book>>.Success(productListModel);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Ошибка при парсинге JSON: {ex.Message}");
                    return ResponseData<ProductListModel<Book>>.Error($"Ошибка: {ex.Message}");
                }
            }

            _logger.LogError($"Не удалось получить данные от сервера. Ошибка: {response.StatusCode}");
            return ResponseData<ProductListModel<Book>>.Error($"Ошибка сервера: {response.StatusCode}");
        }

        public async Task<ResponseData<Book>?> GetBookByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress?.AbsoluteUri}books/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<Book>>(_serializerOptions);
            }

            _logger.LogError($"Не удалось получить объект. Ошибка: {response.StatusCode}");
            return ResponseData<Book>.Error($"Ошибка сервера: {response.StatusCode}");
        }

        public async Task<ResponseData<Book>?> CreateBookAsync(Book book, IFormFile? formFile)
        {
            var uri = new Uri($"{_httpClient.BaseAddress?.AbsoluteUri}books");
            var response = await _httpClient.PostAsJsonAsync(uri, book, _serializerOptions);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<Book>>(_serializerOptions);
            }

            _logger.LogError($"Не удалось создать объект. Ошибка: {response.StatusCode}");
            return ResponseData<Book>.Error($"Ошибка сервера: {response.StatusCode}");
        }

        public async Task UpdateBookAsync(int id, Book book, IFormFile? formFile)
        {
            var uri = new Uri($"{_httpClient.BaseAddress?.AbsoluteUri}books/{id}");
            var response = await _httpClient.PutAsJsonAsync(uri, book, _serializerOptions);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Объект с id {id} успешно обновлён.");
            }
            else
            {
                _logger.LogError($"Не удалось обновить объект. Ошибка: {response.StatusCode}");
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var uri = new Uri($"{_httpClient.BaseAddress?.AbsoluteUri}books/{id}");
            var response = await _httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Объект с id {id} успешно удалён.");
            }
            else
            {
                _logger.LogError($"Не удалось удалить объект. Ошибка: {response.StatusCode}");
            }
        }
    }
}
