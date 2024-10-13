using Microsoft.AspNetCore.Mvc;
using WEB_253504_RESHETNEV.Services.GenreServices;
using WEB_253504_RESHETNEV.Domain.Entities;
using WEB_253504_RESHETNEV.Domain.Models;

namespace WEB_253504_RESHETNEV.Services.BookServices;

public class MemoryBookService : IBookService
{
    private readonly IConfiguration _configuration;
    List<Book> _books;
    List<Genre> _genres;

    public MemoryBookService([FromServices] IConfiguration configuration, IGenreService genreService)
    {
        _configuration = configuration;
        _genres = genreService.GetGenreListAsync().Result.Data;
        SetupData();
    }


    private void SetupData()
    {
        _books = new List<Book>
        {
            new Book
            {
                Id = 1, Name = "Дюна", Description =
                    "Эпическая сага о борьбе за контроль над планетой Арракис, источником ценного ресурса.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                PageCount = 412, ImagePath = "images/duina.jpeg"
            },
            new Book
            {
                Id = 2, Name = "Властелин колец: Братство кольца", Description =
                    "Путешествие хоббита Фродо к уничтожению могущественного кольца, способного подчинить мир тьме.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                PageCount = 423, ImagePath = "images/lotr_fellowship.jpg"
            },
            new Book
            {
                Id = 3, Name = "Гарри Поттер и философский камень", Description =
                    "История юного волшебника, который открывает для себя мир магии и дружбы.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                PageCount = 309, ImagePath = "images/garry_potter.jpeg"
            },
            new Book
            {
                Id = 4, Name = "Убийство в Восточном экспрессе", Description =
                    "Частный детектив Эркюль Пуаро расследует загадочное убийство на роскошном поезде.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("detective")),
                PageCount = 256, ImagePath = "images/kill_east_express.jpeg"
            },
            new Book
            {
                Id = 5, Name = "Девушка с татуировкой дракона", Description =
                    "Журналист и хакер объединяются, чтобы раскрыть тайну исчезновения женщины много лет назад.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("detective")),
                PageCount = 465, ImagePath = "images/women_with_dragon_tatoo.jpeg"
            },
            new Book
            {
                Id = 6, Name = "Гордость и предубеждение", Description =
                    "История о любви и недопонимании между Элизабет Беннет и мистером Дарси на фоне английского общества.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("romance")),
                PageCount = 279, ImagePath = "images/gordost.jpeg"
            },
            new Book
            {
                Id = 7, Name = "Сияние", Description =
                    "Психологическая драма о семье, которая сталкивается с тёмными силами в отдалённом отеле.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("horror")),
                PageCount = 659, ImagePath = "images/siyaniye.jpeg"
            },
            new Book
            {
                Id = 8, Name = "451 градус по Фаренгейту", Description =
                    "В мире, где книги запрещены, пожарный начинает сомневаться в правильности своего выбора.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("sci-fi")),
                PageCount = 249, ImagePath = "images/451.jpeg"
            },
            new Book
            {
                Id = 9, Name = "Война и мир", Description =
                    "Эпопея, охватывающая судьбы нескольких семей на фоне Наполеоновских войн.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("historical")),
                PageCount = 1225, ImagePath = "images/war_and_piece.jpeg"
            },
            new Book
            {
                Id = 10, Name = "Виноваты звезды", Description =
                    "Роман о любви двух подростков, страдающих от рака, и их поисках смысла в жизни.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("young_adult")),
                PageCount = 313, ImagePath = "images/vinovaty_zvezdy.jpeg"
            },
            new Book
            {
                Id = 11, Name = "Сияние", Description =
                    "Семья, оказавшаяся в заброшенном отеле, сталкивается с ужасами своего прошлого и мистическими силами.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("horror")),
                PageCount = 659, ImagePath = "images/siyaniye.jpeg"
            },
            new Book
            {
                Id = 12, Name = "Остров сокровищ", Description =
                    "Молодой Джим Хокин отправляется в опасное путешествие на поиски пиратского золота.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("adventure")),
                PageCount = 240, ImagePath = "images/island_with_gold.jpeg"
            },
            new Book
            {
                Id = 13, Name = "Путешествие на край земли", Description =
                    "Экспедиция к северному полюсу полна опасностей и удивительных открытий.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("adventure")),
                PageCount = 312, ImagePath = "images/trip_to_end_of_world.jpeg"
            },
            new Book
            {
                Id = 14, Name = "Исчезнувшая", Description =
                    "Исследование тёмных сторон брака, когда жена пропадает, и подозрения падают на её мужа.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("psychological_thriller")),
                PageCount = 432, ImagePath = "images/ishcheznuvshaya.jpeg"
            },
            new Book
            {
                Id = 15, Name = "Семь", Description =
                    "Остросюжетная история о человеке, который оказывается в ловушке своего разума и воспоминаний.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("psychological_thriller")),
                PageCount = 368, ImagePath = "images/seven.jpeg"
            },
            new Book
            {
                Id = 16, Name = "Тень ветра", Description =
                    "Молодой мальчик находит загадочную книгу и раскрывает тайны её автора в послевоенной Барселоне.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("historical")),
                PageCount = 487, ImagePath = "images/shadow_of_the_night.jpeg"
            },
            new Book
            {
                Id = 17, Name = "Марсианин", Description =
                    "Астронавт, оставшийся на Марсе, использует свои знания, чтобы выжить и найти способ вернуться на Землю.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("sci-fi")),
                PageCount = 369, ImagePath = "images/marsian.jpeg"
            },
            new Book
            {
                Id = 18, Name = "Гарри Поттер и тайная комната", Description =
                    "Гарри возвращается в Хогвартс, где сталкивается с опасной тайной и зловещими событиями.",
                Genre = _genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                PageCount = 341, ImagePath = "images/harry_and_the_chamber_room.jpeg"
            },
        };
    }

    public Task<ResponseData<ProductListModel<Book>>> GetMovieListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        var itemsPerPage = Convert.ToInt32(_configuration.GetRequiredSection("ItemsPerPage").Value);

        // Добавьте проверку на null для categoryNormalizedName
        var filteredMovies = _books
            .Where(m => categoryNormalizedName == "all" ||
                        (m.Genre != null && m.Genre.NormalizedName != null &&
                         m.Genre.NormalizedName.Equals(categoryNormalizedName)))
            .ToList();

        var totalMovies = filteredMovies.Count;
        var totalPages = (int)Math.Ceiling(totalMovies / (double)itemsPerPage);
        var moviesOnPage = filteredMovies
            .Skip((pageNo - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToList();

        var productListModel = new ProductListModel<Book>
        {
            Items = moviesOnPage,
            CurrentPage = pageNo,
            TotalPages = totalPages,
        };

        var result = ResponseData<ProductListModel<Book>>.Success(productListModel);
        return Task.FromResult(result);
    }


    public Task<ResponseData<Book>> GetMovieByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMovieAsync(int id, Book product, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMovieAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<Book>> CreateMovieAsync(Book product, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}