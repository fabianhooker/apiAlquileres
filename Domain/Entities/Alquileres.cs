using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Alquileres
    {
        public int ID_Alquiler { get; set; }
        public int ID_Usuario { get; set; }
        public int ID_Libro { get; set; }
        public DateTime Fecha_Alquiler { get; set; } = DateTime.Now;
        public DateTime? Fecha_Devolucion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal? Penalidad { get; set; }
        public string? Titulo { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

    }
}
