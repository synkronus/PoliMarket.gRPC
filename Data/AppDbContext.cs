using Microsoft.EntityFrameworkCore;
using PoliMarket.gRPC.Models;

namespace PoliMarket.gRPC.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Precio).HasColumnType("REAL");
        });

        modelBuilder.Entity<Producto>().HasData(
            new Producto { Id = "PROD001", Nombre = "Laptop Dell Inspiron", Descripcion = "Laptop Dell Inspiron 15 3000 Series", Precio = 2500000 },
            new Producto { Id = "PROD002", Nombre = "Mouse Inalámbrico", Descripcion = "Mouse inalámbrico ergonómico", Precio = 45000 },
            new Producto { Id = "PROD003", Nombre = "Monitor Samsung 24\"", Descripcion = "Monitor LED Full HD 24 pulgadas", Precio = 800000 }
        );
    }
}

