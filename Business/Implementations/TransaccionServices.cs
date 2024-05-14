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

namespace Business.Implementations
{
    public class TransaccionServices : ITransaccionServices
    {
        private readonly Tienda_MusicaContext _dbcontext;

        public TransaccionServices(Tienda_MusicaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<TransaccionViews> ConsultarTransacciones()
        {
            List<TransaccionViews> listaTransacciones = new List<TransaccionViews>();
            var transacciones = _dbcontext.Transaccions.ToList();

            if (transacciones != null)
            {
                foreach (var transaccion in transacciones)
                {
                    TransaccionViews transaccionView = new TransaccionViews()
                    {
                        IdTransaccion = transaccion.IdTransaccion,
                        FechaHora = transaccion.FechaHora,
                        TotalCompra = transaccion.TotalCompra,
                        IdCliente = transaccion.IdCliente
                    };
                    listaTransacciones.Add(transaccionView);
                }
            }
            return listaTransacciones;
        }

        public TransaccionViews ConsultarTransaccion(int id)
        {
            var transaccion = _dbcontext.Transaccions.FirstOrDefault(t => t.IdTransaccion == id);
            if (transaccion == null)
            {
                return null; // O lanzar una excepción si lo prefieres
            }

            var transaccionView = new TransaccionViews
            {
                IdTransaccion = transaccion.IdTransaccion,
                FechaHora = transaccion.FechaHora,
                TotalCompra = transaccion.TotalCompra,
                IdCliente = transaccion.IdCliente
            };

            return transaccionView;
        }

        public TransaccionViews CrearTransaccion(TransaccionViews transaccion)
        {
            try
            {
                if (transaccion == null)
                {
                    throw new ArgumentNullException(nameof(transaccion));
                }

                // Crear una nueva transacción con los datos proporcionados
                var nuevaTransaccion = new Transaccion
                {
                    FechaHora = transaccion.FechaHora,
                    TotalCompra = transaccion.TotalCompra,
                    IdCliente = transaccion.IdCliente
                };

                // Agregar la nueva transacción al contexto de la base de datos y guardar los cambios
                _dbcontext.Transaccions.Add(nuevaTransaccion);
                _dbcontext.SaveChanges();

                // Actualizar el IdTransaccion en el objeto TransaccionViews
                transaccion.IdTransaccion = nuevaTransaccion.IdTransaccion;

                // Devolver el objeto TransaccionViews actualizado con el nuevo IdTransaccion
                return transaccion;
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Debug.WriteLine($"Error al crear la transacción: {ex.Message}");
                throw; // Relanzar la excepción para que sea manejada en un nivel superior si es necesario
            }
        }



        public void ActualizarTransaccion(TransaccionViews transaccion)
        {
            try
            {
                if (transaccion == null)
                {
                    throw new ArgumentNullException(nameof(transaccion));
                }

                // Buscar la transacción existente en la base de datos por su ID
                var transaccionExistente = _dbcontext.Transaccions.FirstOrDefault(t => t.IdTransaccion == transaccion.IdTransaccion);

                // Verificar si la transacción existe
                if (transaccionExistente == null)
                {
                    throw new ArgumentException("Transacción no encontrada");
                }

                // Actualizar los campos de la transacción existente con los valores proporcionados en el objeto transacción
                transaccionExistente.FechaHora = transaccion.FechaHora;
                transaccionExistente.TotalCompra = transaccion.TotalCompra;
                transaccionExistente.IdCliente = transaccion.IdCliente;

                // Guardar los cambios en la base de datos
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine($"Error al actualizar la transacción: {ex.Message}");
                throw; // Relanzar la excepción para que sea manejada en un nivel superior si es necesario
            }
        }

        public void EliminarTransaccion(int id)
        {
            var transaccion = _dbcontext.Transaccions.FirstOrDefault(t => t.IdTransaccion == id);
            if (transaccion != null)
            {
                _dbcontext.Transaccions.Remove(transaccion);
                _dbcontext.SaveChanges();
            }
        }
    }
}
