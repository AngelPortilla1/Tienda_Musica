using Core.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IClienteServices
    {
        public List<ClienteViews> ConsultarServicios();
        ClienteViews ConsultarServicio(int id);
        ClienteViews CrearCliente(ClienteViews cliente);
        void ActualizarCliente(ClienteViews cliente);
        void EliminarCliente(int id);


    }       
}
