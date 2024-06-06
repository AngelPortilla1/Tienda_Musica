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
                return null; 
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
                    throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
                }

                // Validar los campos obligatorios del cliente
                if (string.IsNullOrEmpty(cliente.Nombre))
                {
                    throw new ArgumentException("El nombre del cliente es obligatorio.");
                }

                if (string.IsNullOrEmpty(cliente.CorreoElectronico))
                {
                    throw new ArgumentException("El correo electrónico del cliente es obligatorio.");
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
            catch (DbUpdateException ex)
            {
                // Manejar errores relacionados con la base de datos
                Debug.WriteLine($"Error al actualizar la base de datos: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al guardar los cambios en la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Manejar errores relacionados con operaciones inválidas
                Debug.WriteLine($"Operación inválida: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al realizar la operación solicitada.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de errores
                Debug.WriteLine($"Error inesperado: {ex.Message}");
                throw new ApplicationException("Ocurrió un error inesperado al crear el cliente.", ex);
            }
        }


        public void ActualizarCliente(ClienteViews cliente)
        {
            // Buscar el cliente en la base de datos por su ID
            var clienteExistente = _dbcontext.Clientes.FirstOrDefault(c => c.IdCliente == cliente.IdCliente);

            // Verificar si el cliente existe
            if (clienteExistente == null)
            {
                throw new ArgumentException("Cliente no encontrado");
            }

            // Actualizar solo los campos proporcionados en el cliente recibido en la solicitud
            if (!string.IsNullOrEmpty(cliente.Nombre))
            {
                clienteExistente.Nombre = cliente.Nombre;
            }

            if (!string.IsNullOrEmpty(cliente.Apellido))
            {
                clienteExistente.Apellido = cliente.Apellido;
            }

            if (!string.IsNullOrEmpty(cliente.CorreoElectronico))
            {
                clienteExistente.CorreoElectronico = cliente.CorreoElectronico;
            }

            if (!string.IsNullOrEmpty(cliente.Pais))
            {
                clienteExistente.Pais = cliente.Pais;
            }

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
