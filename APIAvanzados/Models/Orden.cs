using System.ComponentModel.DataAnnotations;

namespace APIAvanzados.Models;

public class Orden
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ClienteId debe ser mayor que cero.")]
    public int ClienteId { get; set; }

    public DateTime Fecha { get; set; }

    [Range(0.01, 9999999, ErrorMessage = "El total debe ser mayor que cero.")]
    public decimal Total { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [RegularExpression("^(Pendiente|Pagada|Cancelada|Enviada|Entregada)$",
        ErrorMessage = "El estado debe ser uno de: Pendiente, Pagada, Cancelada, Enviada, Entregada.")]
    public string Estado { get; set; } = string.Empty;
}
