using Microsoft.AspNetCore.Mvc;

namespace api_example_template.Controllers.EndpointsBasicos;

[ApiController]
[Route("api/endpoints-basicos/calculos-comerciales")]
public class CalculosComercialesController : ControllerBase
{
    [HttpGet("subtotal")]
    public ActionResult<decimal> Subtotal(decimal precio, int cantidad)
    {
        if (precio < 0 || cantidad < 0)
        {
            return BadRequest("El precio y la cantidad deben ser mayores o iguales a cero.");
        }

        return Ok(precio * cantidad);
    }

    [HttpGet("descuento")]
    public ActionResult<decimal> Descuento(decimal precio, decimal porcentajeDescuento)
    {
        if (precio < 0 || porcentajeDescuento < 0 || porcentajeDescuento > 100)
        {
            return BadRequest("Los valores enviados no son válidos.");
        }

        return Ok(precio - (precio * porcentajeDescuento / 100));
    }

    [HttpGet("itbis")]
    public ActionResult<decimal> Itbis(decimal monto)
    {
        if (monto < 0)
        {
            return BadRequest("El monto debe ser mayor o igual a cero.");
        }

        return Ok(monto * 0.18m);
    }

    [HttpGet("total-con-itbis")]
    public ActionResult<decimal> TotalConItbis(decimal monto)
    {
        if (monto < 0)
        {
            return BadRequest("El monto debe ser mayor o igual a cero.");
        }

        return Ok(monto + (monto * 0.18m));
    }
}
