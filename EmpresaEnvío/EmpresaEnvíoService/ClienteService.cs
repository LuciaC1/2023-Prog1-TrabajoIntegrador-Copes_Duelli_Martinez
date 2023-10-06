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
            clienteAEditar = ModificarCliente(clienteModificado, clienteAEditar);
            listaClientesDB.RemoveAll(x => x.DNI == clienteAEditar.DNI);
            listaClientesDB.Add(clienteAEditar);
            listaClientesDB = listaClientesDB.OrderBy(x => x.FechaCreacion).ToList();
            archivo.SaveClienteDB(listaClientesDB);
            clienteModificado.DNI = clienteAEditar.DNI;
            clienteModificado.Apellido = clienteAEditar.Apellido;
            clienteModificado.Nombre = clienteAEditar.  Nombre;
            clienteModificado.Email = clienteAEditar.Email;
            clienteModificado.Telefono  = clienteAEditar.Telefono;
            clienteModificado.LongitudGeografica = clienteAEditar.LongitudGeografica;
            clienteModificado.LatitudGeografica= clienteAEditar.LatitudGeografica;
            validCliente.Cliente = clienteModificado;
            validCliente.Resultado = true;
            validCliente.Cliente = new ClienteDto();
            validCliente.Cliente.DNI = clienteAEditar.DNI;
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
                LongitudGeografica = X.LongitudGeografica,
            }).ToList());
        }


        private ClienteDB ModificarCliente(ClienteDto clienteMod, ClienteDB clienteAMod)
        {
            clienteAMod.Nombre = string.IsNullOrEmpty(clienteMod.Nombre) ? clienteMod.Nombre : clienteAMod.Nombre;
            clienteAMod.Apellido = string.IsNullOrEmpty(clienteMod.Apellido) ? clienteMod.Apellido : clienteAMod.Apellido;
            clienteAMod.Email = string.IsNullOrEmpty(clienteMod.Email) ? clienteMod.Email : clienteAMod.Email;
            clienteAMod.Telefono = (clienteMod.Telefono==0) ? clienteMod.Telefono : clienteAMod.Telefono;
            clienteAMod.LatitudGeografica = (clienteMod.LatitudGeografica==0) ? clienteMod.LatitudGeografica : clienteAMod.LatitudGeografica;
            clienteAMod.LongitudGeografica = (clienteMod.LongitudGeografica==0) ? clienteAMod.LongitudGeografica : clienteAMod.LongitudGeografica;
            clienteAMod.FechaActualizacion = DateTime.Now;
            return clienteAMod;
        }
    }
}
