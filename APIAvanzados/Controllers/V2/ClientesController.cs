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
[Route("api/v{version:apiVersion}/clientes")]
[ApiKey]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<RespuestaPaginada<Cliente>>> ObtenerTodos([FromQuery] ParametrosPaginacion parametros)
    {
        var consulta = _context.Clientes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parametros.Buscar))
        {
            var termino = parametros.Buscar.ToLower();
            consulta = consulta.Where(c =>
                c.Nombre.ToLower().Contains(termino) ||
                c.Correo.ToLower().Contains(termino));
        }

        var ascendente = parametros.Direccion.ToLower() != "desc";
        consulta = parametros.OrdenarPor?.ToLower() switch
        {
            "nombre" => ascendente ? consulta.OrderBy(c => c.Nombre) : consulta.OrderByDescending(c => c.Nombre),
            "correo" => ascendente ? consulta.OrderBy(c => c.Correo) : consulta.OrderByDescending(c => c.Correo),
            _ => consulta.OrderBy(c => c.Id)
        };

        var total = await consulta.CountAsync();
        var datos = await consulta
            .Skip((parametros.Pagina - 1) * parametros.TamanoPagina)
            .Take(parametros.TamanoPagina)
            .ToListAsync();

        return Ok(new RespuestaPaginada<Cliente>
        {
            Datos = datos,
            TotalRegistros = total,
            PaginaActual = parametros.Pagina,
            TamanoPagina = parametros.TamanoPagina
        });
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

        return CreatedAtAction(nameof(ObtenerPorId), new { id = cliente.Id, version = "2.0" }, cliente);
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
