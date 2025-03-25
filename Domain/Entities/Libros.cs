namespace Domain.Entities
{
    public class Libros
    {       
            public int ID_Libro { get; set; }
            public string Titulo { get; set; } = string.Empty;
            public string Autor { get; set; } = string.Empty;
            public string? Categoria { get; set; }
            public string? Editorial { get; set; }
            public int? Anio_Publicacion { get; set; }
            public string? ISBN { get; set; }
            public decimal? Precio_Venta { get; set; }
            public string Estado { get; set; } = string.Empty;        
    }
}
