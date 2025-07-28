using Microsoft.EntityFrameworkCore;
using RefugioMimi.Models;

namespace RefugioMimi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Reserva> Reservas => Set<Reserva>();
}
