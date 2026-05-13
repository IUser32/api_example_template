using APIAvanzados.Data;
using APIAvanzados.Models;
using APIAvanzados.Models.Paginacion;
using APIAvanzados.Seguridad;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAvanzados.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/productos")]
[ApiKey]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> ObtenerTodos()
    {
        return Ok(await _context.Productos.ToListAsync());
    }

    [HttpGet("paginado")]
    public async Task<ActionResult<RespuestaPaginada<Producto>>> ObtenerPaginado([FromQuery] ParametrosPaginacion parametros)
    {
        var consulta = _context.Productos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parametros.Buscar))
        {
            var termino = parametros.Buscar.ToLower();
            consulta = consulta.Where(p => p.Nombre.ToLower().Contains(termino));
        }

        var ascendente = parametros.Direccion.ToLower() != "desc";
        consulta = parametros.OrdenarPor?.ToLower() switch
        {
            "nombre" => ascendente ? consulta.OrderBy(p => p.Nombre) : consulta.OrderByDescending(p => p.Nombre),
            "precio" => ascendente ? consulta.OrderBy(p => p.Precio) : consulta.OrderByDescending(p => p.Precio),
            "existencia" => ascendente ? consulta.OrderBy(p => p.Existencia) : consulta.OrderByDescending(p => p.Existencia),
            _ => consulta.OrderBy(p => p.Id)
        };

        var total = await consulta.CountAsync();
        var datos = await consulta
            .Skip((parametros.Pagina - 1) * parametros.TamanoPagina)
            .Take(parametros.TamanoPagina)
            .ToListAsync();

        return Ok(new RespuestaPaginada<Producto>
        {
            Datos = datos,
            TotalRegistros = total,
            PaginaActual = parametros.Pagina,
            TamanoPagina = parametros.TamanoPagina
        });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Producto>> ObtenerPorId(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if (producto is null)
        {
            return NotFound();
        }

        return Ok(producto);
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> Crear(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id, version = "1.0" }, producto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Producto productoActualizado)
    {
        var producto = await _context.Productos.FindAsync(id);

        if (producto is null)
        {
            return NotFound();
        }

        producto.Nombre = productoActualizado.Nombre;
        producto.Precio = productoActualizado.Precio;
        producto.Existencia = productoActualizado.Existencia;
        producto.CategoriaId = productoActualizado.CategoriaId;
        producto.ProveedorId = productoActualizado.ProveedorId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if (producto is null)
        {
            return NotFound();
        }

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
