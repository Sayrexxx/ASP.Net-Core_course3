using WEB_253504_RESHETNEV.Services.ApiServices;
using WEB_253504_RESHETNEV.Services.GenreServices;
using WEB_253504_RESHETNEV.Services.BookServices;


namespace WEB_253504_RESHETNEV.Extensions;

public static class HostingExtensions
{
    public static void RegisterCustomServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<Services.GenreServices.IGenreService, ApiGenreService>();
        builder.Services.AddScoped<Services.BookServices.IBookService, ApiBookService>();
    }
}