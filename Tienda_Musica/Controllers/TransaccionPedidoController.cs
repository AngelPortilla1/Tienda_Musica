using Business.Interfaces;
using Core.ModelsView;
using Infraestructura.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Tienda_Musica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionServices _transaccionServices;

        public TransaccionController(ITransaccionServices transaccionServices)
        {
            _transaccionServices = transaccionServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TransaccionViews>> GetTransacciones()
        {
            var transacciones = _transaccionServices.ConsultarTransacciones();
            return Ok(transacciones);
        }

        [HttpGet("{id}")]
        public ActionResult<TransaccionViews> GetTransaccion(int id)
        {
            var transaccion = _transaccionServices.ConsultarTransaccion(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            return Ok(transaccion);
        }

        // POST api/<TransaccionController>
        [HttpPost]
        public ActionResult<TransaccionViews> CrearTransaccion(TransaccionViews transaccion)
        {
            try
            {
                var nuevaTransaccion = _transaccionServices.CrearTransaccion(transaccion);
                return CreatedAtAction(nameof(GetTransaccion), new { id = nuevaTransaccion.IdTransaccion }, nuevaTransaccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TransaccionViews> ActualizarTransaccion(int id, TransaccionViews transaccion)
        {
            try
            {
                _transaccionServices.ActualizarTransaccion(transaccion);
                return Ok(transaccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarTransaccion(int id)
        {
            try
            {
                _transaccionServices.EliminarTransaccion(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
