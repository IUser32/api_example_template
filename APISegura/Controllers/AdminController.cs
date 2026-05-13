using APISegura.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISegura.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly SeguraDbContext _context;

    public AdminController(SeguraDbContext context)
    {
        _context = context;
    }

    [HttpGet("usuarios")]
    public async Task<IActionResult> ObtenerUsuarios()
    {
        var usuarios = await _context.Usuarios
            .Select(u => new
            {
                u.Id,
                u.Nombre,
                u.NombreUsuario,
                u.Correo,
                u.Rol,
                u.FechaRegistro
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    [HttpGet("tareas")]
    public async Task<IActionResult> ObtenerTodasLasTareas()
    {
        return Ok(await _context.Tareas.ToListAsync());
    }

    [HttpDelete("usuarios/{id:int}")]
    public async Task<IActionResult> EliminarUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario is null)
        {
            return NotFound();
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("usuarios/{id:int}/rol")]
    public async Task<IActionResult> CambiarRol(int id, [FromQuery] string rol)
    {
        if (rol != "Admin" && rol != "Usuario")
        {
            return BadRequest(new { mensaje = "El rol debe ser 'Admin' o 'Usuario'." });
        }

        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario is null)
        {
            return NotFound();
        }

        usuario.Rol = rol;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
