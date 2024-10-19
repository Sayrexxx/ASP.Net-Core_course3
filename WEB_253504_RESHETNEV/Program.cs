using WEB_253504_RESHETNEV.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WEB_253504_RESHETNEV;
using WEB_253504_RESHETNEV.Services.ApiServices;
using WEB_253504_RESHETNEV.Services.BookServices;
using WEB_253504_RESHETNEV.Services.GenreServices;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext") ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));


// Логирование
builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.RegisterCustomServices();


// Получаем конфигурацию
var configuration = builder.Configuration;

var uriData = builder.Configuration.GetSection("UriData").Get<UriData>();

builder.Services.AddHttpClient<IGenreService, ApiGenreService>((serviceProvider, client) =>
    {
        client.BaseAddress = new Uri(uriData!.ApiUri);

        // Отключаем проверку сертификатов (только для разработки)
        client.DefaultRequestVersion = new Version(2, 0); // Добавьте это, чтобы избежать проблем с HTTP/2
        client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample"); // Можно добавить user-agent
    })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });


builder.Services.AddHttpClient<IBookService, ApiBookService>((serviceProvider, client) =>
    {
        client.BaseAddress = new Uri(uriData!.ApiUri);

        client.DefaultRequestVersion = new Version(2, 0);
        client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
    })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });



//
// builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));
// // Регистрация HttpClient для сервисов
// builder.Services.AddHttpClient<IGenreService, ApiGenreService>(opt=> opt.BaseAddress=new Uri(UriData.ApiUri));
// builder.Services.AddHttpClient<IGenreService, ApiGenreService>(opt=> opt.BaseAddress=new Uri(UriData.ApiUri));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();