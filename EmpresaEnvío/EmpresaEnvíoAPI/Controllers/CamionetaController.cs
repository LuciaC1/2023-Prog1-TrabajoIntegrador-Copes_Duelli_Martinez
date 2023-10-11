using EmpresaEnvíoService;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaEnvíoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CamionetaController : ControllerBase
    {
        private CamionetaService service;

        public CamionetaController()
        {
            service = new CamionetaService();
        }

        [HttpGet]
        public IActionResult GetCamionetas()
        {
            return Ok(CamionetaService.ObtenerListadoCamionetas());
        }
    }
}