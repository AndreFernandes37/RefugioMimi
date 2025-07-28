using Microsoft.AspNetCore.Mvc;

namespace RefugioMimi.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Galeria() => View();

    public IActionResult Sobre() => View();
}
