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

        ClienteController()
        {
            service = new ClienteService();
        }
        [HttpPost("")]
        public IActionResult CrearNuevoCliente([FromBody] ClienteDto clienteDto)
        {
            var clienteNuevo = service.CrearCliente(clienteDto);
            //Aca tengo la duda con la validacion, porque no se bien como funciona el required, si te tira el error y te la corta nomas tendria que tirar el ok y el cliente nuevo
            if (clienteNuevo.IsValid)
            {
                return BadRequest("Error en crear suscripcion");
            }

            return Ok(clienteNuevo);
        }
        [HttpPut("")]
        public IActionResult ActualizarCliente([FromBody] ClienteDto clienteModificado)
        {
            var clienteActualizado= service.EditarCliente(clienteModificado);

            if (clienteActualizado==null)
            {
                return BadRequest("No existe el cliente solicitado");
                
            }

            return Ok(clienteActualizado);
        }
        [HttpDelete("{dni}")]
        public IActionResult EliminarSuscripcion(int dni)
        {
            var eliminarSuscripcionResponse = service.EliminarCliente(dni);

            if (eliminarSuscripcionResponse)
            {
                return Ok($"Cliente con DNI {dni} eliminadO exitosamente.");
            }
            return NotFound($"Cliente con DNI  {dni} no encontradO.");
        }
        [HttpGet]
        public IActionResult ObtenerListadoClientes()
        {
            return Ok(service.ObtenerListadoClientes);
        }
    }
}
