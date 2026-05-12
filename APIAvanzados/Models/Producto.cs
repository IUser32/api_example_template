namespace APIAvanzados.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Existencia { get; set; }
    public int CategoriaId { get; set; }
    public int ProveedorId { get; set; }
}
