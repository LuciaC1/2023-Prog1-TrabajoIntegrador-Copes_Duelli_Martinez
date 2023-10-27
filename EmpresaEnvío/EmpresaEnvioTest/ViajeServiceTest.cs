using EmpresaEnvÍoDto;
using EmpresaEnvíoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEnvioTest
{
    public class ViajeServiceTest
    {
        private ViajeService viajeService;
        private ProductoService productoService;
        private ClienteService clienteService;
        private CompraService compraService;

        [SetUp]
        public void Setup()
        {
            clienteService = new ClienteService();
            productoService = new ProductoService();
            compraService = new CompraService();
            viajeService = new ViajeService();
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\viajes.json");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\productos.json");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\compras.json");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\clientes.json");
        }
        [Test]
        public void Add_Viaje_ShouldBeTrue()
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
            ProductoDto productoDto2 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P2",
                MarcaProducto = "M2",
                AltoCaja = 5.0,
                AnchoCaja = 5.0,
                ProfundidadCaja = 5.0,
                PrecioUnitario = 210.0,
                StockMinimo = 5,
                StockTotal = 15,
            };
            ProductoDto productoDto3 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P3",
                MarcaProducto = "M3",
                AltoCaja = 10,
                AnchoCaja = 2,
                ProfundidadCaja = 3,
                PrecioUnitario = 55,
                StockMinimo = 3,
                StockTotal = 25,
            };
            ProductoDto productoDto4 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P4",
                MarcaProducto = "M4",
                AltoCaja = 4,
                AnchoCaja = 7,
                ProfundidadCaja = 4,
                PrecioUnitario = 22,
                StockMinimo = 7,
                StockTotal = 40,
            };
            ProductoDto productoDto5 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P5",
                MarcaProducto = "M5",
                AltoCaja = 10,
                AnchoCaja = 4,
                ProfundidadCaja = 6,
                PrecioUnitario = 35,
                StockMinimo = 2,
                StockTotal = 21,
            };
            ProductoDto productoDto6 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P6",
                MarcaProducto = "M6",
                AltoCaja = 2,
                AnchoCaja = 2,
                ProfundidadCaja = 4,
                PrecioUnitario = 8,
                StockMinimo = 10,
                StockTotal = 84,
            };
            CompraDto compraDto = new CompraDto()
            {
                CodigoProducto = 1,
                CantComprada = 3,
                DNICliente = 1111,
                LatitudGeografica = -31.252,
                LongitudGeografica = -61.489,
                MontoTotal = 798.6,
                FechaEntregaSolicitada = DateTime.Now.AddDays(6),
            };
            CompraDto compraDto2 = new CompraDto()
            {
                CodigoProducto = 2,
                CantComprada = 3,
                DNICliente = 5555,
                FechaEntregaSolicitada = DateTime.Now.AddDays(4),
            };
            CompraDto compraDto3 = new CompraDto()
            {
                CodigoProducto = 4,
                CantComprada = 10,
                DNICliente = 3333,
                FechaEntregaSolicitada = DateTime.Now.AddDays(7),
            };
            CompraDto compraDto4 = new CompraDto()
            {
                CodigoProducto = 4,
                CantComprada = 5,
                DNICliente = 3333,
                FechaEntregaSolicitada = DateTime.Now.AddDays(15),
            };
            CompraDto compraDto5 = new CompraDto()
            {
                CodigoProducto = 1,
                CantComprada = 6,
                DNICliente = 1111,
                FechaEntregaSolicitada = DateTime.Now.AddDays(4),
            };
            CompraDto compraDto6 = new CompraDto()
            {
                CodigoProducto = 3,
                CantComprada = 7,
                DNICliente = 5555,
                FechaEntregaSolicitada = DateTime.Now.AddDays(4),
            };
            CompraDto compraDto7 = new CompraDto()
            {
                CodigoProducto = 6,
                CantComprada = 1,
                DNICliente = 3333,
                FechaEntregaSolicitada = DateTime.Now.AddDays(10),
            };
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 1111,
                Nombre = "PrimerC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 101010,
                LatitudGeografica = -31.252,
                LongitudGeografica = -61.489,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDto2 = new ClienteDto()
            {
                DNI = 2222,
                Nombre = "SegundoC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 202020,
                LatitudGeografica = -32.1,
                LongitudGeografica = -60.2,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDto3 = new ClienteDto()
            {
                DNI = 3333,
                Nombre = "TercerC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 303030,
                LatitudGeografica = -31.1,
                LongitudGeografica = -57.51,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDto4 = new ClienteDto()
            {
                DNI = 4444,
                Nombre = "CuartoC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 404040,
                LatitudGeografica = -31.2,
                LongitudGeografica = -61.51,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDto5 = new ClienteDto()
            {
                DNI = 5555,
                Nombre = "QuintoC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 505050,
                LatitudGeografica = -30,
                LongitudGeografica = -59.51,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };

            productoService.AñadirProducto(productoDto);
            productoService.AñadirProducto(productoDto2);
            productoService.AñadirProducto(productoDto3);
            productoService.AñadirProducto(productoDto4);
            productoService.AñadirProducto(productoDto5);
            productoService.AñadirProducto(productoDto6);
            clienteService.CrearCliente(clienteDto);
            clienteService.CrearCliente(clienteDto2);
            clienteService.CrearCliente(clienteDto3);
            clienteService.CrearCliente(clienteDto4);
            clienteService.CrearCliente(clienteDto5);
            compraService.RegistrarCompra(compraDto);
            compraService.RegistrarCompra(compraDto2);
            compraService.RegistrarCompra(compraDto3);
            compraService.RegistrarCompra(compraDto4);
            compraService.RegistrarCompra(compraDto5);
            compraService.RegistrarCompra(compraDto6);
            compraService.RegistrarCompra(compraDto7);

            var validacion = viajeService.ProgramarEnvío(DateTime.Now.AddMinutes(5), DateTime.Now.AddDays(7));

            Assert.That(validacion.Resultado, Is.True);
        }
        [Test]
        public void Add_Viaje_ShouldBeFalse()
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
            ProductoDto productoDto2 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P2",
                MarcaProducto = "M2",
                AltoCaja = 5.0,
                AnchoCaja = 5.0,
                ProfundidadCaja = 5.0,
                PrecioUnitario = 210.0,
                StockMinimo = 5,
                StockTotal = 15,
            };
            ProductoDto productoDto3 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P3",
                MarcaProducto = "M3",
                AltoCaja = 10,
                AnchoCaja = 2,
                ProfundidadCaja = 3,
                PrecioUnitario = 55,
                StockMinimo = 3,
                StockTotal = 25,
            };
            ProductoDto productoDto4 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P4",
                MarcaProducto = "M4",
                AltoCaja = 4,
                AnchoCaja = 7,
                ProfundidadCaja = 4,
                PrecioUnitario = 22,
                StockMinimo = 7,
                StockTotal = 40,
            };
            ProductoDto productoDto5 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P5",
                MarcaProducto = "M5",
                AltoCaja = 10,
                AnchoCaja = 4,
                ProfundidadCaja = 6,
                PrecioUnitario = 35,
                StockMinimo = 2,
                StockTotal = 21,
            };
            ProductoDto productoDto6 = new ProductoDto()
            {
                CodProducto = 1,
                NombreProducto = "P6",
                MarcaProducto = "M6",
                AltoCaja = 2,
                AnchoCaja = 2,
                ProfundidadCaja = 4,
                PrecioUnitario = 8,
                StockMinimo = 10,
                StockTotal = 84,
            };
            CompraDto compraDto = new CompraDto()
            {
                CodigoProducto = 1,
                CantComprada = 3,
                DNICliente = 1111,
                LatitudGeografica = -31.252,
                LongitudGeografica = -61.489,
                MontoTotal = 798.6,
                FechaEntregaSolicitada = DateTime.Now.AddDays(6),
            };
            CompraDto compraDto2 = new CompraDto()
            {
                CodigoProducto = 2,
                CantComprada = 3,
                DNICliente = 5555,
                FechaEntregaSolicitada = DateTime.Now.AddDays(4),
            };
            CompraDto compraDto3 = new CompraDto()
            {
                CodigoProducto = 4,
                CantComprada = 10,
                DNICliente = 3333,
                FechaEntregaSolicitada = DateTime.Now.AddDays(7),
            };
            CompraDto compraDto4 = new CompraDto()
            {
                CodigoProducto = 4,
                CantComprada = 5,
                DNICliente = 3333,
                FechaEntregaSolicitada = DateTime.Now.AddDays(15),
            };
            CompraDto compraDto5 = new CompraDto()
            {
                CodigoProducto = 1,
                CantComprada = 6,
                DNICliente = 1111,
                FechaEntregaSolicitada = DateTime.Now.AddDays(4),
            };
            CompraDto compraDto6 = new CompraDto()
            {
                CodigoProducto = 3,
                CantComprada = 7,
                DNICliente = 5555,
                FechaEntregaSolicitada = DateTime.Now.AddDays(4),
            };
            CompraDto compraDto7 = new CompraDto()
            {
                CodigoProducto = 6,
                CantComprada = 1,
                DNICliente = 3333,
                FechaEntregaSolicitada = DateTime.Now.AddDays(10),
            };
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 1111,
                Nombre = "PrimerC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 101010,
                LatitudGeografica = -31.252,
                LongitudGeografica = -61.489,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDto2 = new ClienteDto()
            {
                DNI = 2222,
                Nombre = "SegundoC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 202020,
                LatitudGeografica = -32.1,
                LongitudGeografica = -60.2,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDto3 = new ClienteDto()
            {
                DNI = 3333,
                Nombre = "TercerC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 303030,
                LatitudGeografica = -31.1,
                LongitudGeografica = -57.51,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDto4 = new ClienteDto()
            {
                DNI = 4444,
                Nombre = "CuartoC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 404040,
                LatitudGeografica = -31.2,
                LongitudGeografica = -61.51,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDto5 = new ClienteDto()
            {
                DNI = 5555,
                Nombre = "QuintoC",
                Apellido = "Cliente",
                Email = "cli.com",
                Telefono = 505050,
                LatitudGeografica = -30,
                LongitudGeografica = -59.51,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };

            productoService.AñadirProducto(productoDto);
            productoService.AñadirProducto(productoDto2);
            productoService.AñadirProducto(productoDto3);
            productoService.AñadirProducto(productoDto4);
            productoService.AñadirProducto(productoDto5);
            productoService.AñadirProducto(productoDto6);
            clienteService.CrearCliente(clienteDto);
            clienteService.CrearCliente(clienteDto2);
            clienteService.CrearCliente(clienteDto3);
            clienteService.CrearCliente(clienteDto4);
            clienteService.CrearCliente(clienteDto5);
            compraService.RegistrarCompra(compraDto);
            compraService.RegistrarCompra(compraDto2);
            compraService.RegistrarCompra(compraDto3);
            compraService.RegistrarCompra(compraDto4);
            compraService.RegistrarCompra(compraDto5);
            compraService.RegistrarCompra(compraDto6);
            compraService.RegistrarCompra(compraDto7);

            var validacion = viajeService.ProgramarEnvío(DateTime.Now.AddDays(1000), DateTime.Now.AddDays(1005));

            Assert.That(validacion.Resultado, Is.False);
        }
    }
}
