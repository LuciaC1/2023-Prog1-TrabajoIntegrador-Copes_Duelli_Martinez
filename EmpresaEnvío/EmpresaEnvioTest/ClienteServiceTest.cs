using EmpresaEnvíoService;
using EmpresaEnvÍoDto;
using EmpresaEnvíoData;

using System;

namespace EmpresaEnvioTest

{
    public class ClienteServiceTest
    {
        ClienteService service;
        [SetUp]
        public void Setup()
        {
            service = new ClienteService();
        }

        [Test]
        public void Delete_Cliente_ShouldBeTrue()
        {
            ClienteDto clienteDto = new ClienteDto()
            {
                DNI=44305328,
                Nombre="Santiago",
                Apellido="Martinez",
                Email="santmartinez48@gmail.com",
                Telefono=349269420,
                LatitudGeografica= -31.25033,
                LongitudGeografica= -61.4867,
                FechaNacimiento=DateTime.Now,
            };

            service.CrearCliente(clienteDto);
            var validacion=service.EliminarCliente(44305328);

            Assert.That(validacion.Resultado, Is.True);
        }
    }
}