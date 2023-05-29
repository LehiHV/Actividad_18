using Actividad_18.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Actividad_18.Server.Contexto
{
    public class ContextoConstructora : DbContext
    {
        public ContextoConstructora(DbContextOptions<ContextoConstructora> options) : base(options) 
        {
            
        }
        public DbSet<Trabajadores> Trabajadores { get; set; }
        public DbSet<Edificios> Edificios { get; set; }
        public DbSet<Comandas> Comandas { get; set; }
    }
}
