using APIAvanzados.Data;
using APIAvanzados.Models;
using APIAvanzados.Models.Paginacion;
using APIAvanzados.Seguridad;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAvanzados.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/ordenes")]
[ApiKey]
public class OrdenesController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdenesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<RespuestaPaginada<Orden>>> ObtenerTodas([FromQuery] ParametrosPaginacion parametros)
    {
        var consulta = _context.Ordenes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parametros.Buscar))
        {
            var termino = parametros.Buscar.ToLower();
            consulta = consulta.Where(o => o.Estado.ToLower().Contains(termino));
        }

        var ascendente = parametros.Direccion.ToLower() != "desc";
        consulta = parametros.OrdenarPor?.ToLower() switch
        {
            "fecha" => ascendente ? consulta.OrderBy(o => o.Fecha) : consulta.OrderByDescending(o => o.Fecha),
            "total" => ascendente ? consulta.OrderBy(o => o.Total) : consulta.OrderByDescending(o => o.Total),
            "estado" => ascendente ? consulta.OrderBy(o => o.Estado) : consulta.OrderByDescending(o => o.Estado),
            _ => consulta.OrderBy(o => o.Id)
        };

        var total = await consulta.CountAsync();
        var datos = await consulta
            .Skip((parametros.Pagina - 1) * parametros.TamanoPagina)
            .Take(parametros.TamanoPagina)
            .ToListAsync();

        return Ok(new RespuestaPaginada<Orden>
        {
            Datos = datos,
            TotalRegistros = total,
            PaginaActual = parametros.Pagina,
            TamanoPagina = parametros.TamanoPagina
        });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Orden>> ObtenerPorId(int id)
    {
        var orden = await _context.Ordenes.FindAsync(id);

        if (orden is null)
        {
            return NotFound();
        }

        return Ok(orden);
    }

    [HttpPost]
    public async Task<ActionResult<Orden>> Crear(Orden orden)
    {
        orden.Fecha = orden.Fecha == default ? DateTime.Now : orden.Fecha;

        _context.Ordenes.Add(orden);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPorId), new { id = orden.Id, version = "2.0" }, orden);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Orden ordenActualizada)
    {
        var orden = await _context.Ordenes.FindAsync(id);

        if (orden is null)
        {
            return NotFound();
        }

        orden.ClienteId = ordenActualizada.ClienteId;
        orden.Fecha = ordenActualizada.Fecha;
        orden.Total = ordenActualizada.Total;
        orden.Estado = ordenActualizada.Estado;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var orden = await _context.Ordenes.FindAsync(id);

        if (orden is null)
        {
            return NotFound();
        }

        _context.Ordenes.Remove(orden);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
