using System;
using System.Collections.Generic;

namespace Infraestructura.Models
{
    public partial class ProductoMusical
    {
        public ProductoMusical()
        {
            IdTransaccions = new HashSet<Transaccion>();
        }

        public int IdProducto { get; set; }
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string? Genero { get; set; }
        public decimal? Precio { get; set; }
        public string? Formato { get; set; }
        // FORANEA DE TRANSACCION 
        public virtual ICollection<Transaccion> IdTransaccions { get; set; }
    }
}
