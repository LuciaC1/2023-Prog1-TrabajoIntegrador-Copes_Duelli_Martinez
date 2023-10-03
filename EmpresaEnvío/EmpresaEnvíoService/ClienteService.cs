using EmpresaEnvíoData;
using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class ClienteService
    {
        ArchivoCliente archivo;
        public ClienteService()
        {
            archivo = new ArchivoCliente();
        }
        public ClienteDto CrearCliente(ClienteDto clienteDto)
        {
            List<ClienteDB> listaClientes = archivo.GetClienteDBList();
            ClienteDB clienteDB = new ClienteDB()
            {
                DNI = clienteDto.DNI,
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                Email = clienteDto.Email,
                Telefono = clienteDto.Telefono,
                LatitudGeografica = clienteDto.LatitudGeografica,
                LongitudGeografica = clienteDto.LongitudGeografica,
                FechaNacimiento = clienteDto.FechaNacimiento,
                FechaCreacion = DateTime.Now,
            };
            listaClientes.Add(clienteDB);
            archivo.SaveClienteDB(listaClientes);
            return clienteDto;
        }
        public Validacion EliminarCliente(int dni)
        {
            Validacion validacion = new Validacion();
            List<ClienteDB> listaClientesDB = archivo.GetClienteDBList();
            ClienteDB cliente = listaClientesDB.FirstOrDefault(x => x.DNI == dni);
            if (cliente == null)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El cliente a eliminar no existe" });
                return validacion;
            }
            if (cliente.FechaEliminacion != DateTime.MinValue)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El cliente ya ha sido eliminado previamente" });
                return validacion;
            }
            listaClientesDB.FirstOrDefault(x => x.DNI == dni).FechaEliminacion = DateTime.Now;
            archivo.SaveClienteDB(listaClientesDB);
            validacion.Resultado = true;
            return validacion;
        }
        public ValidacionModCliente EditarCliente(ClienteDto clienteModificado)
        {
            ValidacionModCliente validCliente = new ValidacionModCliente();
            List<ClienteDB> listaClientesDB = archivo.GetClienteDBList();
            var clienteAEditar = listaClientesDB.FirstOrDefault(u => u.DNI == clienteModificado.DNI);
            if (clienteAEditar == default && clienteAEditar.FechaEliminacion != DateTime.MinValue)
            {
                validCliente.Errores.Add(new Error() { ErrorDetail = "El cliente a editar no existe" });
                return validCliente;
            }
            //Validacion de errores

            if (validCliente.Errores.Count > 0)
            {
                return validCliente;
            }
            validCliente.Cliente = clienteModificado;
            validCliente.Resultado = true;
            return validCliente;
        }
        public List<ClienteDto> ObtenerListadoClientes()
        {
            return (archivo.GetClienteDBList().Select(X => new ClienteDto()
            {
                DNI = X.DNI,
                Nombre = X.Nombre,
                Apellido = X.Apellido,
                Email = X.Email,
                Telefono = X.Telefono,
                LatitudGeografica = X.LatitudGeografica,
                LongitudGeografica = X.LongitudGeografica
            }).ToList());
        }
    }
}
