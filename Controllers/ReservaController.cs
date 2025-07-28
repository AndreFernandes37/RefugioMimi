using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugioMimi.Data;
using RefugioMimi.Models;
using RefugioMimi.Services;
using Stripe.Checkout;

namespace RefugioMimi.Controllers;

public class ReservaController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _email;
    private readonly IConfiguration _config;

    public ReservaController(ApplicationDbContext context, IEmailService email, IConfiguration config)
    {
        _context = context;
        _email = email;
        _config = config;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(Reserva reserva)
    {
        if (!ModelState.IsValid)
            return View(reserva);

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        var domain = $"{Request.Scheme}://{Request.Host}";
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 10000,
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Reserva Ref\u00fagio da Mimi"
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = $"{domain}/Reserva/Confirmacao?sessionId={{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"{domain}/Reservar"
        };
        var service = new SessionService();
        var session = await service.CreateAsync(options);

        reserva.StripePaymentId = session.Id;
        await _context.SaveChangesAsync();

        return Redirect(session.Url!);
    }

    [HttpGet]
    public async Task<IActionResult> Confirmacao(string sessionId)
    {
        var reserva = await _context.Reservas.FirstOrDefaultAsync(r => r.StripePaymentId == sessionId);
        if (reserva == null) return NotFound();
        reserva.Estado = "Confirmado";
        await _context.SaveChangesAsync();

        await _email.SendConfirmationAsync(reserva.Email, "Confirma\u00e7\u00e3o de Reserva", $"<p>Sua reserva est\u00e1 confirmada para {reserva.DataEntrada:d} a {reserva.DataSaida:d}</p>");
        return View(reserva);
    }
}
