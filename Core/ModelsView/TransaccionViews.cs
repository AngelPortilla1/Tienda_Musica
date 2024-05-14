using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ModelsView
{
    public class TransaccionViews
    {
        public int IdTransaccion { get; set; }
        public DateTime? FechaHora { get; set; }
        public decimal? TotalCompra { get; set; }
        public int? IdCliente { get; set; }
    }
}
