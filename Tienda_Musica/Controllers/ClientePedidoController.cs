using Business.Implementations;
using Business.Interfaces;
using Core.ModelsView;
using Infraestructura.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tienda_Musica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientePedidoController : ControllerBase
    {

        private readonly IClienteServices _icliente;
        public ClientePedidoController(IClienteServices icliente)
        {
            _icliente = icliente;
        }

        [HttpGet]
        public ActionResult GET()
        {
            try
            {
                var listServicio = _icliente.ConsultarServicios();
                return Ok(listServicio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //GET: api/<ClientePedidoController>
        [HttpGet("{id}")]
        public ActionResult<ClienteViews> GetCliente(int id)
        {
            var cliente = _icliente.ConsultarServicio(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
        


        // POST api/<ClientePedidoController>
        [HttpPost]
        public ActionResult<ClienteViews> CrearCliente(ClienteViews cliente)
        {
            var nuevoCliente = _icliente.CrearCliente(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = nuevoCliente.IdCliente }, nuevoCliente);
        }

        // PUT api/<ClientePedidoController>/5
        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(int id, ClienteViews cliente)
        {
            try
            {
                // Llamar al servicio para consultar el cliente existente por su ID
                var clienteExistente = _icliente.ConsultarServicio(id);

                // Verificar si el cliente existe
                if (clienteExistente == null)
                {
                    return NotFound("Cliente no encontrado"); // Devolver error si el cliente no existe
                }

                // Actualizar solo los campos proporcionados en el cliente recibido en la solicitud
                if (cliente.Nombre != null)
                {
                    clienteExistente.Nombre = cliente.Nombre;
                }

                if (cliente.Apellido != null)
                {
                    clienteExistente.Apellido = cliente.Apellido;
                }

                if (cliente.CorreoElectronico != null)
                {
                    clienteExistente.CorreoElectronico = cliente.CorreoElectronico;
                }

                if (cliente.Pais != null)
                {
                    clienteExistente.Pais = cliente.Pais;
                }

                // Llamar al servicio para actualizar el cliente
                _icliente.ActualizarCliente(clienteExistente);

                // Devolver el cliente actualizado
                return Ok(clienteExistente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar actualizar el cliente: {ex.Message}");
            }
        }

        // DELETE api/<ClientePedidoController>/5
        [HttpDelete("{id}")]
        public IActionResult EliminarCliente(int id)
        {
            try
            {
                _icliente.EliminarCliente(id);
                return NoContent(); // Se eliminó con éxito
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Error al intentar eliminar el cliente
            }
        }
    }
        
    }
