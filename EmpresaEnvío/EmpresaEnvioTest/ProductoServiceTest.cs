using EmpresaEnvíoData;
using EmpresaEnvÍoDto;
using EmpresaEnvíoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var validacion = service.ActualizarStock(1,1);

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
        }
    }
}
