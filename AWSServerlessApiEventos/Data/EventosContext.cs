using AWSServerlessApiEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace AWSServerlessApiEventos.Data
{
    public class EventosContext : DbContext
    {
        public EventosContext(DbContextOptions<EventosContext> options) : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
