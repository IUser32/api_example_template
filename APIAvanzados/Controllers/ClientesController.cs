using APIAvanzados.Data;
using APIAvanzados.Models;
using APIAvanzados.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAvanzados.Controllers;

[ApiController]
[Route("api/clientes")]
[ApiKey]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> ObtenerTodos()
    {
        return Ok(await _context.Clientes.ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cliente>> ObtenerPorId(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente is null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Crear(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPorId), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Cliente clienteActualizado)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente is null)
        {
            return NotFound();
        }

        cliente.Nombre = clienteActualizado.Nombre;
        cliente.Correo = clienteActualizado.Correo;
        cliente.Telefono = clienteActualizado.Telefono;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente is null)
        {
            return NotFound();
        }

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
