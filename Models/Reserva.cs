namespace RefugioMimi.Models;

public class Reserva
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Hospedes { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime DataSaida { get; set; }
    public string? Mensagem { get; set; }
    public string Estado { get; set; } = "Pendente"; // ou Confirmado
    public string StripePaymentId { get; set; } = string.Empty;
}
