using Core.ModelsView;
using Infraestructura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITransaccionServices
    {

        public List<TransaccionViews> ConsultarTransacciones();
        TransaccionViews ConsultarTransaccion(int id);
        TransaccionViews CrearTransaccion(TransaccionViews transaccion);
        void ActualizarTransaccion(TransaccionViews transaccion);
        void EliminarTransaccion(int id);
    }
}
