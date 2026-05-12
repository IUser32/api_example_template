using api_example_template.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_example_template.Controllers.EndpointsBasicos;

[ApiController]
[Route("api/endpoints-basicos/prestamos")]
public class PrestamosController : ControllerBase
{
    [HttpGet("interes-simple")]
    public ActionResult<decimal> InteresSimple(decimal monto, decimal tasaAnual, int anios)
    {
        if (monto < 0 || tasaAnual < 0 || anios < 0)
        {
            return BadRequest("Los valores enviados no son válidos.");
        }

        return Ok(monto * (tasaAnual / 100) * anios);
    }

    [HttpPost("cuota-mensual")]
    public ActionResult<decimal> CuotaMensual(CalculoPrestamo prestamo)
    {
        if (prestamo.Monto <= 0 || prestamo.TasaAnual < 0 || prestamo.Meses <= 0)
        {
            return BadRequest("Los valores enviados no son válidos.");
        }

        var tasaMensual = prestamo.TasaAnual / 100 / 12;

        if (tasaMensual == 0)
        {
            return Ok(prestamo.Monto / prestamo.Meses);
        }

        var potencia = (decimal)Math.Pow((double)(1 + tasaMensual), prestamo.Meses);
        var cuota = prestamo.Monto * tasaMensual * potencia / (potencia - 1);

        return Ok(decimal.Round(cuota, 2));
    }
}
