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
            if (clienteDto.IsValid().Resultado) {
                var clienteNuevo = service.CrearCliente(clienteDto);
                return Ok(clienteNuevo);
            }
            return BadRequest(clienteDto.IsValid().Errores);
        }
        [HttpPut("")]
        public IActionResult ActualizarCliente([FromBody] ClienteDto clienteModificado)
        {
            var clienteActualizado = service.EditarCliente(clienteModificado);

            if (clienteActualizado.Resultado == false)
            {
                return BadRequest(clienteActualizado.Errores);
            }

            return Ok(clienteActualizado.Cliente);
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
