namespace APIAvanzados.Models.Paginacion;

public class RespuestaPaginada<T>
{
    public IEnumerable<T> Datos { get; set; } = Array.Empty<T>();
    public int TotalRegistros { get; set; }
    public int PaginaActual { get; set; }
    public int TamanoPagina { get; set; }
    public int TotalPaginas => (int)Math.Ceiling(TotalRegistros / (double)TamanoPagina);
    public bool TienePaginaAnterior => PaginaActual > 1;
    public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
}
