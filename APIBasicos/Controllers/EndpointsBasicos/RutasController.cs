using Microsoft.AspNetCore.Mvc;

namespace api_example_template.Controllers.EndpointsBasicos;

[ApiController]
[Route("api/endpoints-basicos/rutas")]
public class RutasController : ControllerBase
{
    [HttpGet("saludo")]
    public ActionResult<string> SaludoPorQuery(string nombre)
    {
        return Ok($"Hola, {nombre}");
    }

    [HttpGet("saludo/{nombre}")]
    public ActionResult<string> SaludoPorRuta(string nombre)
    {
        return Ok($"Hola, {nombre}");
    }

    [HttpGet("productos/{categoria}/{id:int}")]
    public ActionResult<object> ProductoPorCategoria(string categoria, int id)
    {
        return Ok(new { Categoria = categoria, ProductoId = id });
    }

    [HttpGet("clientes/{clienteId:int}/ordenes/{ordenId:int}")]
    public ActionResult<object> OrdenDeCliente(int clienteId, int ordenId)
    {
        return Ok(new { ClienteId = clienteId, OrdenId = ordenId });
    }

    [HttpGet("buscar/{texto?}")]
    public ActionResult<object> Buscar(string? texto)
    {
        return Ok(new { Texto = texto ?? "Sin texto de busqueda" });
    }

    [HttpGet("fechas/{anio:int}/{mes:int:range(1,12)}/{dia:int:range(1,31)}")]
    public ActionResult<object> FechaPorSegmentos(int anio, int mes, int dia)
    {
        return Ok(new { Anio = anio, Mes = mes, Dia = dia });
    }

    [HttpGet("/api/estado")]
    public ActionResult<object> Estado()
    {
        return Ok(new { Servicio = "API de ejemplo", Estado = "Activo" });
    }
}
