using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253504_RESHETNEV.Models;

namespace WEB_253504_RESHETNEV.Controllers;

public class HomeController : Controller
{
    [ViewData] public string Text { get; set; } = "Лабораторная работа №3";

    public IActionResult Index()
    {
        var data = new List<ListDemo> { new ListDemo { Id = 1, Name = "Item 1" },
            new ListDemo { Id = 2, Name = "Item 2" },
            new ListDemo { Id = 3, Name = "Item 3"} };
        var list = new SelectList(data, nameof(ListDemo.Id), nameof(ListDemo.Name));

        return View(list);
    }
}