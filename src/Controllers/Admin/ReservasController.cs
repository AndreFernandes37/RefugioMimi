using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugioMimi.Data;
using RefugioMimi.Models;

namespace RefugioMimi.Controllers.Admin;

[Area("Admin")]
public class ReservasController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReservasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var reservas = await _context.Reservas.ToListAsync();
        return View(reservas);
    }

    [HttpPost]
    public async Task<IActionResult> AlterarEstado(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null) return NotFound();
        reserva.Estado = reserva.Estado == "Pendente" ? "Confirmado" : "Pendente";
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Exportar()
    {
        var reservas = await _context.Reservas.ToListAsync();
        var csv = "Id,Nome,Email,Hospedes,DataEntrada,DataSaida,Estado\n" + string.Join("\n", reservas.Select(r => $"{r.Id},{r.Nome},{r.Email},{r.Hospedes},{r.DataEntrada:yyyy-MM-dd},{r.DataSaida:yyyy-MM-dd},{r.Estado}"));
        return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", "reservas.csv");
    }
}
