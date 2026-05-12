using api_example_template.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_example_template.Controllers.EndpointsBasicos;

[ApiController]
[Route("api/endpoints-basicos/estadisticas")]
public class EstadisticasController : ControllerBase
{
    [HttpPost("sumatoria")]
    public ActionResult<decimal> Sumatoria(ListaNumeros lista)
    {
        if (lista.Numeros is null || lista.Numeros.Count == 0)
        {
            return BadRequest("Debe enviar al menos un número.");
        }

        return Ok(lista.Numeros.Sum());
    }

    [HttpPost("promedio")]
    public ActionResult<decimal> Promedio(ListaNumeros lista)
    {
        if (lista.Numeros is null || lista.Numeros.Count == 0)
        {
            return BadRequest("Debe enviar al menos un número.");
        }

        return Ok(lista.Numeros.Average());
    }

    [HttpPost("mayor")]
    public ActionResult<decimal> Mayor(ListaNumeros lista)
    {
        if (lista.Numeros is null || lista.Numeros.Count == 0)
        {
            return BadRequest("Debe enviar al menos un número.");
        }

        return Ok(lista.Numeros.Max());
    }

    [HttpPost("menor")]
    public ActionResult<decimal> Menor(ListaNumeros lista)
    {
        if (lista.Numeros is null || lista.Numeros.Count == 0)
        {
            return BadRequest("Debe enviar al menos un número.");
        }

        return Ok(lista.Numeros.Min());
    }
}
