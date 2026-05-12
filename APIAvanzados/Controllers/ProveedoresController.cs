using APIAvanzados.Data;
using APIAvanzados.Models;
using APIAvanzados.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAvanzados.Controllers;

[ApiController]
[Route("api/proveedores")]
[ApiKey]
public class ProveedoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProveedoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedor>>> ObtenerTodos()
    {
        return Ok(await _context.Proveedores.ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Proveedor>> ObtenerPorId(int id)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);

        if (proveedor is null)
        {
            return NotFound();
        }

        return Ok(proveedor);
    }

    [HttpPost]
    public async Task<ActionResult<Proveedor>> Crear(Proveedor proveedor)
    {
        _context.Proveedores.Add(proveedor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPorId), new { id = proveedor.Id }, proveedor);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Proveedor proveedorActualizado)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);

        if (proveedor is null)
        {
            return NotFound();
        }

        proveedor.Nombre = proveedorActualizado.Nombre;
        proveedor.Correo = proveedorActualizado.Correo;
        proveedor.Telefono = proveedorActualizado.Telefono;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);

        if (proveedor is null)
        {
            return NotFound();
        }

        _context.Proveedores.Remove(proveedor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
