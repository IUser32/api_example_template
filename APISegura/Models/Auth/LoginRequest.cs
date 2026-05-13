using System.ComponentModel.DataAnnotations;

namespace APISegura.Models.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contrasena es obligatoria.")]
    public string Contrasena { get; set; } = string.Empty;
}
