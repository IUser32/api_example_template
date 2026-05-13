using System.ComponentModel.DataAnnotations;

namespace APISegura.Models;

public class Tarea
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El titulo es obligatorio.")]
    [StringLength(150, MinimumLength = 3)]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Descripcion { get; set; }

    public bool Completada { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public int UsuarioId { get; set; }
}
