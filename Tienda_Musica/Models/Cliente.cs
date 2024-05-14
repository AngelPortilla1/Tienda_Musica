using System;
using System.Collections.Generic;

namespace Tienda_Musica.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Transaccions = new HashSet<Transaccion>();
        }

        public int IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Pais { get; set; }

        public virtual ICollection<Transaccion> Transaccions { get; set; }
    }
}
