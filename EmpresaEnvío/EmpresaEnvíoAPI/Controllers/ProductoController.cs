using EmpresaEnvÍoDto;
using EmpresaEnvíoService;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaEnvíoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private ProductoService service;

        public ProductoController()
        {
            service = new ProductoService();
        }

        [HttpGet("")]
        public IActionResult ProductosStockMenor()
        {
            return Ok(service.StockMínimo());
        }


        [HttpPost("")]
        public IActionResult AgregarProducto([FromBody] ProductoDto productoDto)
        {
            var validación = productoDto.IsValid();
            if (!validación.Resultado)
            {
                return BadRequest(validación.Errores);
            }
            ValidacionProducto validacionProducto = service.AñadirProducto(productoDto);
            if (!validacionProducto.Resultado)
            {
                return BadRequest(validacionProducto.Errores);
            }
            return Ok(validacionProducto.Producto);
        }

        [HttpPut("{codProducto}")]
        public IActionResult ActualizarStock(int codProducto, [FromBody] int stockNuevo)
        {
            var productoActualizado = service.ActualizarStock(codProducto, stockNuevo);
            if (!productoActualizado.Resultado)
            {
                return BadRequest(productoActualizado.Errores);
            }
            return Ok(productoActualizado.Producto);
        }
    }
}