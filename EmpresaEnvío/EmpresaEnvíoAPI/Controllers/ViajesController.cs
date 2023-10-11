using EmpresaEnvÍoDto;
using EmpresaEnvíoService;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaEnvíoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ViajesController : ControllerBase
    {
        private ViajeService viajeService;

        public ViajesController()
        {
            viajeService = new ViajeService();
        }

        [HttpPost("{fechaDesde}&{fechaHasta}")]
        public IActionResult ProgramarEnvío(DateTime fechaDesde, DateTime fechaHasta)
        {
            ValidacionViaje resultado = viajeService.ProgramarEnvío(fechaDesde, fechaHasta);
            if (!resultado.Resultado)
            {
                return BadRequest(resultado.Errores);
            }
            return Ok(resultado.Viaje);
        }
    }
}