using System.ComponentModel.DataAnnotations;

namespace APIAvanzados.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato valido.")]
    [StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El telefono es obligatorio.")]
    [Phone(ErrorMessage = "El telefono no tiene un formato valido.")]
    [StringLength(20)]
    public string Telefono { get; set; } = string.Empty;
}
