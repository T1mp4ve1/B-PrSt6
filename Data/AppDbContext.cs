using Evento.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Evento.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Artista> Artisti { get; set; }
        public DbSet<Biglietto> Biglietti { get; set; }
        public DbSet<EventoModel> Eventi { get; set; }
    }
}
