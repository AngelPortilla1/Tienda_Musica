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
            try
            {
                // Llamar al servicio para crear el cliente
                var nuevoCliente = _icliente.CrearCliente(cliente);

                // Devolver respuesta Created con el nuevo cliente
                return CreatedAtAction(nameof(GetCliente), new { id = nuevoCliente.IdCliente }, nuevoCliente);
            }
            catch (ArgumentException ex)
            {
                // Manejar errores específicos de argumentos inválidos
                return BadRequest($"Error de validación: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de errores
                return BadRequest($"Error al intentar crear el cliente: {ex.Message}");
            }
        }


        // PUT api/<ClientePedidoController>/5
        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(int id, ClienteViews cliente)
        {
            try
            {
                // Asegurarse de que el ID del cliente coincida
                if (id != cliente.IdCliente)
                {
                    return BadRequest("El ID del cliente no coincide con el ID de la URL.");
                }

                // Llamar al servicio para actualizar el cliente
                _icliente.ActualizarCliente(cliente);

                // Devolver el cliente actualizado
                return Ok(new { message = "Cliente actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
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
