using Microsoft.AspNetCore.Mvc;

namespace api_example_template.Controllers.EndpointsBasicos;

[ApiController]
[Route("api/endpoints-basicos/operaciones")]
public class OperacionesController : ControllerBase
{
    [HttpGet("sumar")]
    public ActionResult<decimal> Sumar(decimal numero1, decimal numero2)
    {
        return Ok(numero1 + numero2);
    }

    [HttpGet("restar")]
    public ActionResult<decimal> Restar(decimal numero1, decimal numero2)
    {
        return Ok(numero1 - numero2);
    }

    [HttpGet("multiplicar")]
    public ActionResult<decimal> Multiplicar(decimal numero1, decimal numero2)
    {
        return Ok(numero1 * numero2);
    }

    [HttpGet("dividir")]
    public ActionResult<decimal> Dividir(decimal numero1, decimal numero2)
    {
        if (numero2 == 0)
        {
            return BadRequest("No se puede dividir entre cero.");
        }

        return Ok(numero1 / numero2);
    }

    [HttpGet("promedio")]
    public ActionResult<decimal> Promedio(decimal numero1, decimal numero2, decimal numero3)
    {
        return Ok((numero1 + numero2 + numero3) / 3);
    }
}
