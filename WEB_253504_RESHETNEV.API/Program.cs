using Microsoft.EntityFrameworkCore;
using WEB_253504_RESHETNEV.API.Data;
using WEB_253504_RESHETNEV.API.Services.BookServices;
using WEB_253504_RESHETNEV.API.Services.GenreServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();


builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();


// Получение строки подключения из конфигурации
var connectionString = builder.Configuration.GetConnectionString("Default");

// Регистрация контекста базы данных с использованием SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


// Использование маршрутов для контроллеров
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Заполнение базы данных исходными данными
await DbInitializer.SeedData(app);

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}