using EmpresaEnvÍoDto;
using EmpresaEnvíoService;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaEnvíoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        ClienteService service;

        public ClienteController()
        {
            service = new ClienteService();
        }
        [HttpPost("")]
        public IActionResult CrearNuevoCliente([FromBody] ClienteDto clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var clienteNuevo = service.CrearCliente(clienteDto);
            if (clienteNuevo == null)
            {
                return BadRequest("Error en crear cliente");
            }

            return Ok(clienteNuevo);
        }
        [HttpPut("")]
        public IActionResult ActualizarCliente([FromBody] ClienteDto clienteModificado)
        {
            var clienteActualizado = service.EditarCliente(clienteModificado);

            if (clienteActualizado == null)
            {
                return BadRequest("No existe el cliente solicitado");

            }

            return Ok(clienteActualizado);
        }
        [HttpDelete("{dni}")]
        public IActionResult EliminarSuscripcion(int dni)
        {
            var eliminarSuscripcionResponse = service.EliminarCliente(dni);

            if (eliminarSuscripcionResponse.Resultado)
            {
                return Ok($"Cliente con DNI {dni} eliminado exitosamente");
            }
            return NotFound($"Cliente con DNI {dni} no encontrado");
        }
        [HttpGet]
        public IActionResult ObtenerListadoClientes()
        {
            return Ok(service.ObtenerListadoClientes);
        }
    }
}
