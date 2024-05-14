using System;
using System.Collections.Generic;

namespace Tienda_Musica.Models
{
    public partial class Transaccion
    {
        public Transaccion()
        {
            IdProductos = new HashSet<ProductoMusical>();
        }

        public int IdTransaccion { get; set; }
        public DateTime? FechaHora { get; set; }
        public decimal? TotalCompra { get; set; }
        public int? IdCliente { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }

        //fORANEA DE PRODUCTO MUSICAL
        public virtual ICollection<ProductoMusical> IdProductos { get; set; }
    }
}
