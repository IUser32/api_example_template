using APIAvanzados.Data;
using APIAvanzados.Models;
using APIAvanzados.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAvanzados.Controllers;

[ApiController]
[Route("api/categorias")]
[ApiKey]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> ObtenerTodas()
    {
        return Ok(await _context.Categorias.ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Categoria>> ObtenerPorId(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria is null)
        {
            return NotFound();
        }

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Crear(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPorId), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Categoria categoriaActualizada)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria is null)
        {
            return NotFound();
        }

        categoria.Nombre = categoriaActualizada.Nombre;
        categoria.Descripcion = categoriaActualizada.Descripcion;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria is null)
        {
            return NotFound();
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
