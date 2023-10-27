using EmpresaEnvÍoDto;
using EmpresaEnvíoService;

namespace EmpresaEnvioTest
{
    public class ClienteServiceTest
    {
        private ClienteService service;

        [SetUp]
        public void Setup()
        {
            service = new ClienteService();
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Archivos" + "\\clientes.json");
        }
        [Test]
        public void Delete_Cliente_ShouldBeTrue()
        {
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 12345,
                Nombre = "Juan",
                Apellido = "Juancho",
                Email = "mail@gmail.com",
                Telefono = 34926563,
                LatitudGeografica = -31.25033,
                LongitudGeografica = -61.4867,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };

            service.CrearCliente(clienteDto);
            var validacion = service.EliminarCliente(12345);

            Assert.That(validacion.Resultado, Is.True);
            Assert.That(validacion.Errores, Is.Empty);
        }

        [Test]
        public void Delete_Cliente_ShouldBeFalse()
        {
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 12345,
                Nombre = "Juan",
                Apellido = "Juancho",
                Email = "mail@gmail.com",
                Telefono = 34926563,
                LatitudGeografica = -31.25033,
                LongitudGeografica = -61.4867,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };

            service.CrearCliente(clienteDto);
            service.EliminarCliente(12345);
            var validacion = service.EliminarCliente(12345);

            Assert.That(validacion.Resultado, Is.False);
            Assert.That(validacion.Errores[0].ErrorDetail, Is.EqualTo("El cliente de dni 12345 ya ha sido eliminado previamente"));
        }
        [Test]
        public void Add_Cliente_ShouldBeTrue()
        {
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 1234567,
                Nombre = "Juan",
                Apellido = "Juancho",
                Email = "mail@gmail.com",
                Telefono = 34926563,
                LatitudGeografica = -31.25033,
                LongitudGeografica = -61.4867,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };

            var validacion = service.CrearCliente(clienteDto);

            Assert.That(validacion.Resultado, Is.True);
            Assert.That(validacion.Errores, Is.Empty);
        }
        [Test]
        public void Add_Cliente_ShouldBeFalse()
        {
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 1234567,
                Nombre = "Juan",
                Apellido = "Juancho",
                Email = "mail@gmail.com",
                Telefono = 34926563,
                LatitudGeografica = -31.25033,
                LongitudGeografica = -61.4867,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };

            service.CrearCliente(clienteDto);
            var validacion = service.CrearCliente(clienteDto);

            Assert.That(validacion.Resultado, Is.False);
            Assert.That(validacion.Errores[0].ErrorDetail, Is.EqualTo("El cliente con ese DNI ya existe"));
        }
        [Test]
        public void Update_Cliente_ShouldBeTrue()
        {
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI = 1234567,
                Nombre = "Juan",
                Apellido = "Juancho",
                Email = "mail@gmail.com",
                Telefono = 34926563,
                LatitudGeografica = -31.25033,
                LongitudGeografica = -61.4867,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };
            ClienteDto clienteDtoNuevo = new ClienteDto()
            {
                DNI = 1234567,
                Nombre = "Pepe",
                Apellido = "Juancho",
                Email = "mail@gmail.com",
                Telefono = 34926563,
                LatitudGeografica = -31.25033,
                LongitudGeografica = -61.4867,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };

            service.CrearCliente(clienteDto);
            var validacion = service.EditarCliente(1234567, clienteDtoNuevo);

            Assert.That(validacion.Resultado, Is.True);
            Assert.That(validacion.Errores, Is.Empty);
        }
        [Test]
        public void Update_Cliente_ShouldBeFalse()
        {
            ClienteDto clienteDtoNuevo = new ClienteDto()
            {
                DNI = 1234567,
                Nombre = "Pepe",
                Apellido = "Juancho",
                Email = "mail@gmail.com",
                Telefono = 34926563,
                LatitudGeografica = -31.25033,
                LongitudGeografica = -61.4867,
                FechaNacimiento = new DateTime(2000, 11, 15)
            };

            var validacion = service.EditarCliente(123456789, clienteDtoNuevo);

            Assert.That(validacion.Resultado, Is.False);
            Assert.That(validacion.Errores[0].ErrorDetail, Is.EqualTo("El cliente a editar no existe o fue eliminado"));
        }
    }
}
