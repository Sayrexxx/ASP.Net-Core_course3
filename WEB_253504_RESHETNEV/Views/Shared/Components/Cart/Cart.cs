using Microsoft.AspNetCore.Mvc;

namespace WEB_253504_RESHETNEV.Views.Components;

public class Cart : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}