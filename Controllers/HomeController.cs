using Microsoft.AspNetCore.Mvc;

namespace RefugioMimi.Controllers;

public class HomeController : Controller
{
    [Route("")]
    public IActionResult Index() => View();

    [Route("/home/sobre")]
    public IActionResult Sobre() => View();
}
