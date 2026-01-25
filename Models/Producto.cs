using System.ComponentModel.DataAnnotations;

namespace PoliMarket.gRPC.Models;

public class Producto
{
    [Key]
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    public double Precio { get; set; }
}

