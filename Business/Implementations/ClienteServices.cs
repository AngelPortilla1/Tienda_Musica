using Business.Implementations;
using Business.Interfaces;
using Core.ModelsView;
using Infraestructura.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Core.ModelsView;
namespace Business.Implementations

{
    public class ClienteServices : IClienteServices
    {
        private readonly Tienda_MusicaContext _dbcontext;
        public ClienteServices() { }

        public ClienteServices(Tienda_MusicaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public List<ClienteViews> ConsultarServicios()
        {
            List<ClienteViews> ListaClienteViews = new List<ClienteViews>();
            var listServicios = _dbcontext.Clientes.ToList();

            if (listServicios != null)
            {
                foreach (var item in listServicios)
                {
                    ClienteViews Clienteviews = new ClienteViews()
                    {
                        IdCliente = item.IdCliente,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        CorreoElectronico = item.CorreoElectronico,
                        Pais =  item.Pais,

                    };
                    ListaClienteViews.Add(Clienteviews);
                }
            }
            return ListaClienteViews;
        }
        public ClienteViews ConsultarServicio(int id)
        {
            var cliente = _dbcontext.Clientes.FirstOrDefault(c => c.IdCliente == id);
            if (cliente == null)
            {
                return null; // O puedes lanzar una excepción si lo prefieres
            }

            var clienteView = new ClienteViews
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                CorreoElectronico = cliente.CorreoElectronico,
                Pais = cliente.Pais,

            };

            return clienteView;
        }
        public ClienteViews CrearCliente(ClienteViews cliente)
        {
            try
            {
                if (cliente == null)
                {
                    throw new ArgumentNullException(nameof(cliente));
                }

                // Determinar el próximo ID disponible
                int proximoIdCliente = _dbcontext.Clientes.Any() ? _dbcontext.Clientes.Max(c => c.IdCliente) + 1 : 1;

                // Depurar para verificar el valor del próximo ID
                Debug.WriteLine($"Próximo ID de cliente: {proximoIdCliente}");

                var nuevoCliente = new Cliente
                {
                    IdCliente = proximoIdCliente,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    CorreoElectronico = cliente.CorreoElectronico,
                    Pais = cliente.Pais
                };

                _dbcontext.Clientes.Add(nuevoCliente);
                _dbcontext.SaveChanges();

                // Devolver el nuevo cliente con el ID generado correctamente
                cliente.IdCliente = nuevoCliente.IdCliente;
                return cliente;
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Debug.WriteLine($"Error al crear el cliente: {ex.Message}");
                throw; // Relanzar la excepción para que sea manejada en un nivel superior si es necesario
            }
        }

        // ClienteServices

        public void ActualizarCliente(ClienteViews cliente)
        {
            // Buscar el cliente en la base de datos por su ID
            var clienteExistente = _dbcontext.Clientes.FirstOrDefault(c => c.IdCliente == cliente.IdCliente);

            // Verificar si el cliente existe
            if (clienteExistente == null)
            {
                throw new ArgumentException("Cliente no encontrado");
            }

            // Actualizar los campos del cliente existente con los valores proporcionados en el objeto cliente
            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.CorreoElectronico = cliente.CorreoElectronico;
            clienteExistente.Pais = cliente.Pais;

            // Guardar los cambios en la base de datos
            _dbcontext.SaveChanges();
        }

        public void EliminarCliente(int id)
        {
            var cliente = _dbcontext.Clientes.FirstOrDefault(c => c.IdCliente == id);
            if (cliente != null)
            {
                _dbcontext.Clientes.Remove(cliente);
                _dbcontext.SaveChanges();
            }
        }






    }
}
