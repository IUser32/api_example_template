namespace APIAvanzados.Models.Paginacion;

public class ParametrosPaginacion
{
    private const int TamanoMaximo = 100;
    private int _tamanoPagina = 10;

    public int Pagina { get; set; } = 1;

    public int TamanoPagina
    {
        get => _tamanoPagina;
        set => _tamanoPagina = value > TamanoMaximo ? TamanoMaximo : value;
    }

    public string? Buscar { get; set; }

    public string? OrdenarPor { get; set; }

    public string Direccion { get; set; } = "asc";
}
