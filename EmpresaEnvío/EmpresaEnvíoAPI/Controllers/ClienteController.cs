using EmpresaEnvÍoDto;
using EmpresaEnvíoService;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaEnvíoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private ClienteService service;

        public ClienteController()
        {
            service = new ClienteService();
        }

        [HttpPost("")]
        public IActionResult CrearNuevoCliente([FromBody] ClienteDto clienteDto)
        {
            Validacion valid = clienteDto.IsValid();
            if (!valid.Resultado)
            {
                return BadRequest(valid.Errores);
            }
            var clienteNuevo = service.CrearCliente(clienteDto);
            if (!clienteNuevo.Resultado)
            {
                return BadRequest(clienteNuevo.Errores);
            }
            return Ok(clienteNuevo.Cliente);
        }

        [HttpPut("")]
        public IActionResult ActualizarCliente([FromBody] ClienteDto clienteModificado)
        {
            Validacion valid = clienteModificado.IsValid();
            if (!valid.Resultado)
            {
                return BadRequest(valid.Errores);
            }
            ValidacionCliente validacionCliente = service.EditarCliente(clienteModificado);

            if (!validacionCliente.Resultado)
            {
                return BadRequest(validacionCliente.Errores);
            }
            return Ok(validacionCliente.Cliente);
        }

        [HttpDelete("{dni}")]
        public IActionResult EliminarCliente(int dni)
        {
            Validacion validacion = service.EliminarCliente(dni);

            if (validacion.Resultado && validacion.Errores.Count > 0)
            {
                return BadRequest(validacion.Errores);
            }
            if (!validacion.Resultado)
            {
                return NotFound(validacion.Errores);
            }
            return Ok($"Cliente con DNI {dni} eliminado correctamente");
        }

        [HttpGet]
        public IActionResult ObtenerListadoClientes()
        {
            return Ok(service.ObtenerListadoClientes());
        }
    }
}