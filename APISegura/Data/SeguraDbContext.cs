using APISegura.Models;
using Microsoft.EntityFrameworkCore;

namespace APISegura.Data;

public class SeguraDbContext : DbContext
{
    public SeguraDbContext(DbContextOptions<SeguraDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Tarea> Tareas => Set<Tarea>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.NombreUsuario)
            .IsUnique();

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Correo)
            .IsUnique();
    }
}
