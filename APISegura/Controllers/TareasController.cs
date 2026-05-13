using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using APISegura.Data;
using APISegura.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISegura.Controllers;

[ApiController]
[Authorize]
[Route("api/tareas")]
public class TareasController : ControllerBase
{
    private readonly SeguraDbContext _context;

    public TareasController(SeguraDbContext context)
    {
        _context = context;
    }

    private int UsuarioIdActual =>
        int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerMisTareas()
    {
        var tareas = await _context.Tareas
            .Where(t => t.UsuarioId == UsuarioIdActual)
            .ToListAsync();

        return Ok(tareas);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Tarea>> ObtenerPorId(int id)
    {
        var tarea = await _context.Tareas
            .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == UsuarioIdActual);

        if (tarea is null)
        {
            return NotFound();
        }

        return Ok(tarea);
    }

    [HttpPost]
    public async Task<ActionResult<Tarea>> Crear(Tarea tarea)
    {
        tarea.UsuarioId = UsuarioIdActual;
        tarea.FechaCreacion = DateTime.UtcNow;

        _context.Tareas.Add(tarea);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPorId), new { id = tarea.Id }, tarea);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Tarea tareaActualizada)
    {
        var tarea = await _context.Tareas
            .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == UsuarioIdActual);

        if (tarea is null)
        {
            return NotFound();
        }

        tarea.Titulo = tareaActualizada.Titulo;
        tarea.Descripcion = tareaActualizada.Descripcion;
        tarea.Completada = tareaActualizada.Completada;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var tarea = await _context.Tareas
            .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == UsuarioIdActual);

        if (tarea is null)
        {
            return NotFound();
        }

        _context.Tareas.Remove(tarea);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
