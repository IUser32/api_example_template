using api_example_template.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_example_template.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private static readonly List<Producto> Productos =
    [
        new Producto { Id = 1, Nombre = "Laptop", Precio = 45000, CantidadDisponible = 8 },
        new Producto { Id = 2, Nombre = "Mouse", Precio = 850, CantidadDisponible = 25 },
        new Producto { Id = 3, Nombre = "Teclado", Precio = 2200, CantidadDisponible = 15 }
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Producto>> ObtenerTodos()
    {
        return Ok(Productos);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        var producto = Productos.FirstOrDefault(producto => producto.Id == id);

        if (producto is null)
        {
            return NotFound();
        }

        return Ok(producto);
    }

    [HttpPost]
    public ActionResult<Producto> Crear(Producto producto)
    {
        producto.Id = Productos.Max(producto => producto.Id) + 1;
        Productos.Add(producto);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
    }

    [HttpPut("{id:int}")]
    public IActionResult Actualizar(int id, Producto productoActualizado)
    {
        var producto = Productos.FirstOrDefault(producto => producto.Id == id);

        if (producto is null)
        {
            return NotFound();
        }

        producto.Nombre = productoActualizado.Nombre;
        producto.Precio = productoActualizado.Precio;
        producto.CantidadDisponible = productoActualizado.CantidadDisponible;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        var producto = Productos.FirstOrDefault(producto => producto.Id == id);

        if (producto is null)
        {
            return NotFound();
        }

        Productos.Remove(producto);

        return NoContent();
    }
}
