using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ModelsView
{
    public class ProductoMusical
    {
        public int IdProducto { get; set; }
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string? Genero { get; set; }
        public decimal? Precio { get; set; }
        public string? Formato { get; set; }
    }
}
