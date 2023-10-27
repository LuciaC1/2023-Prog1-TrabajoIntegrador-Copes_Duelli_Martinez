using EmpresaEnvÍoDto;
using EmpresaEnvíoService;

namespace EmpresaEnvioTest
{
    public class ProductoeServiceTest
    {
        private ProductoService service;

        [SetUp]
        public void Setup()
        {
            service = new ProductoService();
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\productos.json");
        }
        [Test]
        public void Add_Producto_ShouldBeTrue()
        {
            ProductoDto productoDto = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P1",
                MarcaProducto = "M1",
                AltoCaja = 10.0,
                AnchoCaja = 5.0,
                ProfundidadCaja = 5.0,
                PrecioUnitario = 220.0,
                StockMinimo = 10,
                StockTotal = 22,
            };

            var validacion = service.AñadirProducto(productoDto);

            Assert.That(validacion.Resultado, Is.True);
            Assert.That(validacion.Errores, Is.Empty);
        }
        [Test]
        public void Add_Producto_ShouldBeFalse()
        {
            ProductoDto productoDto = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P1",
                MarcaProducto = "M1",
                AltoCaja = 10.0,
                AnchoCaja = 5.0,
                ProfundidadCaja = 5.0,
                PrecioUnitario = 220.0,
                StockMinimo = 0,
                StockTotal = 0,
            };

            var validacion = service.AñadirProducto(productoDto);

            Assert.That(validacion.Resultado, Is.False);
            Assert.That(validacion.Errores[0].ErrorDetail, Is.EqualTo("El stock mínimo debe ser mayor a cero"));
        }
        [Test]
        public void Update_Producto_ShouldBeTrue()
        {
            ProductoDto productoDto = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P1",
                MarcaProducto = "M1",
                AltoCaja = 10.0,
                AnchoCaja = 5.0,
                ProfundidadCaja = 5.0,
                PrecioUnitario = 220.0,
                StockMinimo = 3,
                StockTotal = 22,
            };

            service.AñadirProducto(productoDto);
            var validacion = service.ActualizarStock(1, 1);

            Assert.That(validacion.Resultado, Is.True);
        }
        [Test]
        public void Update_Producto_ShouldBeFale()
        {
            ProductoDto productoDto = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P1",
                MarcaProducto = "M1",
                AltoCaja = 10.0,
                AnchoCaja = 5.0,
                ProfundidadCaja = 5.0,
                PrecioUnitario = 220.0,
                StockMinimo = 3,
                StockTotal = 22,
            };

            service.AñadirProducto(productoDto);
            var validacion = service.ActualizarStock(1, -1);

            Assert.That(validacion.Resultado, Is.False);
            Assert.That(validacion.Errores[0].ErrorDetail, Is.EqualTo("El producto no puede tener un stock negativo"));
        }
    }
}
