using EmpresaEnvÍoDto;
using EmpresaEnvíoService;

namespace EmpresaEnvioTest
{
    public class CompraServiceTest
    {
        private CompraService compraService;
        private ProductoService productoService;
        private ClienteService clienteService;

        [SetUp]
        public void Setup()
        {
            productoService = new ProductoService();
            compraService = new CompraService();
            clienteService = new ClienteService();
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\compras.json");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\productos.json");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\clientes.json");
        }
        [Test]
        public void Add_Compra_ShouldBeTrue()
        {
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 1111,
                Nombre = "PrimerC",
                Apellido = "Cliente",
                Email = "clichange.com",
                Telefono = 101011,
                LatitudGeografica = -31.252,
                LongitudGeografica = -61.489,
                FechaNacimiento = new DateTime(1989, 10, 17)
            };
            CompraDto compraDto = new CompraDto()
            {
                CodigoProducto = 1,
                CantComprada = 3,
                DNICliente = 1111,
                FechaEntregaSolicitada = new DateTime().AddDays(6),
            };
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

            clienteService.CrearCliente(clienteDto);
            productoService.AñadirProducto(productoDto);
            var validacion = compraService.RegistrarCompra(compraDto);

            Assert.That(validacion.Resultado, Is.True);
            Assert.That(validacion.Errores, Is.Empty);
        }
        [Test]
        public void Add_Compra_ShouldBeFalse()
        {
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 1111,
                Nombre = "PrimerC",
                Apellido = "Cliente",
                Email = "clichange.com",
                Telefono = 101011,
                LatitudGeografica = -31.252,
                LongitudGeografica = -61.489,
                FechaNacimiento = new DateTime(1989, 10, 17)
            };
            CompraDto compraDto = new CompraDto()
            {
                CodigoProducto = 1,
                CantComprada = 3,
                DNICliente = 1111,
                FechaEntregaSolicitada = new DateTime().AddDays(6),
            };

            clienteService.CrearCliente(clienteDto);
            var validacion = compraService.RegistrarCompra(compraDto);

            Assert.That(validacion.Resultado, Is.False);
            Assert.That(validacion.Errores[0].ErrorDetail, Is.EqualTo("El producto a comprar no existe"));
        }
    }
}

