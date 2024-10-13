using Microsoft.AspNetCore.Mvc;

namespace WEB_253504_RESHETNEV.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}