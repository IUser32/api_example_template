using APIAvanzados.Data;
using APIAvanzados.Models;
using APIAvanzados.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAvanzados.Controllers;

[ApiController]
[Route("api/ordenes")]
[ApiKey]
public class OrdenesController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdenesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Orden>>> ObtenerTodas()
    {
        return Ok(await _context.Ordenes.ToListAsync());
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

        return CreatedAtAction(nameof(ObtenerPorId), new { id = orden.Id }, orden);
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
