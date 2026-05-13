using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APISegura.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/publico")]
public class PublicoController : ControllerBase
{
    [HttpGet("saludo")]
    public IActionResult Saludo()
    {
        return Ok(new { mensaje = "Hola, este endpoint es publico y no requiere autenticacion." });
    }

    [HttpGet("info")]
    public IActionResult Info()
    {
        return Ok(new
        {
            servicio = "APISegura",
            autenticacion = "JWT Bearer",
            documentacion = "/swagger"
        });
    }
}
