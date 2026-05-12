using APIAvanzados.Seguridad;
using Microsoft.AspNetCore.Mvc;

namespace APIAvanzados.Controllers;

[ApiController]
[Route("api/seguridad")]
public class SeguridadController : ControllerBase
{
    [HttpGet("publico")]
    public ActionResult<object> Publico()
    {
        return Ok(new { Mensaje = "Este endpoint no requiere API Key." });
    }

    [ApiKey]
    [HttpGet("privado")]
    public ActionResult<object> Privado()
    {
        return Ok(new { Mensaje = "Este endpoint requiere API Key." });
    }
}
