using Microsoft.AspNetCore.Mvc;

namespace RefugioMimi.Controllers;

[Route("galeria")]
public class GaleriaController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
