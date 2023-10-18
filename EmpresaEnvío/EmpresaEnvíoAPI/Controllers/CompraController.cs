using EmpresaEnvÍoDto;
using EmpresaEnvíoService;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaEnvíoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private CompraService compraService;

        public CompraController()
        {
            compraService = new CompraService();
        }

        [HttpPost]
        public IActionResult AltaCompra([FromBody] CompraDto compra)
        {
            Validacion validacion = compra.IsValid();
            if (!validacion.Resultado)
            {
                return BadRequest(validacion.Errores);
            }
            var resultado = compraService.RegistrarCompra(compra);
            if (!resultado.Resultado)
            {
                return BadRequest(resultado.Errores);
            }
            return Ok(resultado.Compra);
        }
    }
}