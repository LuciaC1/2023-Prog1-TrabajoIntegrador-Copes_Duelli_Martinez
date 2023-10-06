using EmpresaEnvÍoDto;
using EmpresaEnvíoService;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaEnvíoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        ProductoService service;

        public ProductoController()
        {
            service = new ProductoService();
        }
        [HttpPost("")]
        public IActionResult AgregarProducto([FromBody] ProductoDto productoDto)
        {
            if (productoDto.IsValid().Resultado)
            {
                service.AñadirProducto(productoDto);
                return Ok(productoDto);
            }
            return BadRequest(productoDto.IsValid().Errores);
        }
        [HttpPut("")]
        public IActionResult ActualizarStock([FromBody] int codProducto, int stockNuevo)
        {
            var productoActualizado = service.ActualizarStock(codProducto, stockNuevo);
            if (productoActualizado.Resultado == false)
            {
                return BadRequest(productoActualizado.Errores[1]);
            }
            return Ok(productoActualizado.Producto);
        }
    }
}
