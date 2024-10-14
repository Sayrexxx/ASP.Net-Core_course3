using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEB_253504_RESHETNEV.API.Data; // Замените на правильный namespace вашего контекста
using WEB_253504_RESHETNEV.Domain.Entities; // Замените на namespace, где находятся ваши модели

public static class DbInitializer
{
    public static async Task SeedData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Выполнение миграций
        await context.Database.MigrateAsync();

        // Проверка на наличие данных в таблицах
        if (!context.Genres.Any() && !context.Books.Any())
        {
            // Получение URL для изображений из appsettings.json
            var configuration = app.Configuration;
            var baseUrl = configuration["BaseUrl"];

            // Создание исходных данных для жанров
            var genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Фантастика", NormalizedName = "sci-fi" },
                new Genre { Id = 2, Name = "Фэнтези", NormalizedName = "fantasy" },
                new Genre { Id = 3, Name = "Детектив", NormalizedName = "detective" },
                new Genre { Id = 4, Name = "Романтика", NormalizedName = "romance" },
                new Genre { Id = 5, Name = "Научная фантастика", NormalizedName = "sci-fi" },
                new Genre { Id = 6, Name = "Исторический роман", NormalizedName = "historical" },
                new Genre { Id = 7, Name = "Ужасы", NormalizedName = "horror" },
                new Genre { Id = 8, Name = "Приключения", NormalizedName = "adventure" },
                new Genre { Id = 9, Name = "Психологический триллер", NormalizedName = "psychological_thriller" },
                new Genre { Id = 10, Name = "Молодежная литература", NormalizedName = "young_adult" }
            };

            // Добавление данных для жанров в контекст
            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();

            // Создание исходных данных для книг
            var books = new List<Book>
            {
                 new Book
            {
                Id = 1, Name = "Дюна", Description =
                    "Эпическая сага о борьбе за контроль над планетой Арракис, источником ценного ресурса.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                PageCount = 412, ImagePath = $"{baseUrl}/Images/duina.jpeg"
            },
            new Book
            {
                Id = 2, Name = "Властелин колец: Братство кольца", Description =
                    "Путешествие хоббита Фродо к уничтожению могущественного кольца, способного подчинить мир тьме.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                PageCount = 423, ImagePath = $"{baseUrl}/Images/lotr_fellowship.jpg"
            },
            new Book
            {
                Id = 3, Name = "Гарри Поттер и философский камень", Description =
                    "История юного волшебника, который открывает для себя мир магии и дружбы.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                PageCount = 309, ImagePath = $"{baseUrl}/Images/garry_potter.jpeg"
            },
            new Book
            {
                Id = 4, Name = "Убийство в Восточном экспрессе", Description =
                    "Частный детектив Эркюль Пуаро расследует загадочное убийство на роскошном поезде.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("detective")),
                PageCount = 256, ImagePath = $"{baseUrl}/Images/kill_east_express.jpeg"
            },
            new Book
            {
                Id = 5, Name = "Девушка с татуировкой дракона", Description =
                    "Журналист и хакер объединяются, чтобы раскрыть тайну исчезновения женщины много лет назад.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("detective")),
                PageCount = 465, ImagePath = $"{baseUrl}/Images/women_with_dragon_tatoo.jpeg"
            },
            new Book
            {
                Id = 6, Name = "Гордость и предубеждение", Description =
                    "История о любви и недопонимании между Элизабет Беннет и мистером Дарси на фоне английского общества.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("romance")),
                PageCount = 279, ImagePath = $"{baseUrl}/Images/gordost.jpeg"
            },
            new Book
            {
                Id = 7, Name = "Сияние", Description =
                    "Психологическая драма о семье, которая сталкивается с тёмными силами в отдалённом отеле.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("horror")),
                PageCount = 659, ImagePath = $"{baseUrl}/Images/siyaniye.jpeg"
            },
            new Book
            {
                Id = 8, Name = "451 градус по Фаренгейту", Description =
                    "В мире, где книги запрещены, пожарный начинает сомневаться в правильности своего выбора.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("sci-fi")),
                PageCount = 249, ImagePath = $"{baseUrl}/Images/451.jpeg"
            },
            new Book
            {
                Id = 9, Name = "Война и мир", Description =
                    "Эпопея, охватывающая судьбы нескольких семей на фоне Наполеоновских войн.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("historical")),
                PageCount = 1225, ImagePath = $"{baseUrl}/Images/war_and_piece.jpeg"
            },
            new Book
            {
                Id = 10, Name = "Виноваты звезды", Description =
                    "Роман о любви двух подростков, страдающих от рака, и их поисках смысла в жизни.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("young_adult")),
                PageCount = 313, ImagePath = $"{baseUrl}/Images/vinovaty_zvezdy.jpeg"
            },
            new Book
            {
                Id = 11, Name = "Сияние", Description =
                    "Семья, оказавшаяся в заброшенном отеле, сталкивается с ужасами своего прошлого и мистическими силами.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("horror")),
                PageCount = 659, ImagePath = $"{baseUrl}/Images/siyaniye.jpeg"
            },
            new Book
            {
                Id = 12, Name = "Остров сокровищ", Description =
                    "Молодой Джим Хокин отправляется в опасное путешествие на поиски пиратского золота.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("adventure")),
                PageCount = 240, ImagePath = $"{baseUrl}/Images/island_with_gold.jpeg"
            },
            new Book
            {
                Id = 13, Name = "Путешествие на край земли", Description =
                    "Экспедиция к северному полюсу полна опасностей и удивительных открытий.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("adventure")),
                PageCount = 312, ImagePath = $"{baseUrl}/Images/trip_to_end_of_world.jpeg"
            },
            new Book
            {
                Id = 14, Name = "Исчезнувшая", Description =
                    "Исследование тёмных сторон брака, когда жена пропадает, и подозрения падают на её мужа.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("psychological_thriller")),
                PageCount = 432, ImagePath = $"{baseUrl}/Images/ishcheznuvshaya.jpeg"
            },
            new Book
            {
                Id = 15, Name = "Семь", Description =
                    "Остросюжетная история о человеке, который оказывается в ловушке своего разума и воспоминаний.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("psychological_thriller")),
                PageCount = 368, ImagePath = $"{baseUrl}/Images/seven.jpeg"
            },
            new Book
            {
                Id = 16, Name = "Тень ветра", Description =
                    "Молодой мальчик находит загадочную книгу и раскрывает тайны её автора в послевоенной Барселоне.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("historical")),
                PageCount = 487, ImagePath = $"{baseUrl}/Images/shadow_of_the_night.jpeg"
            },
            new Book
            {
                Id = 17, Name = "Марсианин", Description =
                    "Астронавт, оставшийся на Марсе, использует свои знания, чтобы выжить и найти способ вернуться на Землю.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("sci-fi")),
                PageCount = 369, ImagePath = $"{baseUrl}/Images/marsian.jpeg"
            },
            new Book
            {
                Id = 18, Name = "Гарри Поттер и тайная комната", Description =
                    "Гарри возвращается в Хогвартс, где сталкивается с опасной тайной и зловещими событиями.",
                Genre = genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                PageCount = 341, ImagePath = $"{baseUrl}/Images/harry_and_the_chamber_room.jpeg"
            },
            };

            // Добавление данных для книг в контекст
            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }
    }
}
