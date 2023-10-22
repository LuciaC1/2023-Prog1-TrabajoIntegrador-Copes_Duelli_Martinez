using EmpresaEnvíoData;
using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class ClienteService
    {
        #region Constructor

        private ArchivoCliente archivo;

        public ClienteService()
        {
            archivo = new ArchivoCliente();
        }

        #endregion Constructor

        //Crear cliente (POST)
        public ValidacionCliente CrearCliente(ClienteDto clienteDto)
        {
            ValidacionCliente validaciónCliente = new ValidacionCliente();
            var listaClientes = archivo.GetClienteDBList();
            if (listaClientes.Any(x => x.DNI == clienteDto.DNI))
            {
                validaciónCliente.Errores.Add(new Error() { ErrorDetail = "El cliente con ese DNI ya existe" });
                validaciónCliente.Resultado = false;
                return validaciónCliente;
            }
            ClienteDB clienteDB = new()
            {
                DNI = clienteDto.DNI,
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                Email = clienteDto.Email,
                Telefono = clienteDto.Telefono,
                LatitudGeografica = clienteDto.LatitudGeografica,
                LongitudGeografica = clienteDto.LongitudGeografica,
                FechaNacimiento = clienteDto.FechaNacimiento,
                FechaCreacion = DateTime.Now
            };
            listaClientes.Add(clienteDB);
            archivo.SaveClienteDB(listaClientes);
            validaciónCliente.Cliente = clienteDto;
            validaciónCliente.Resultado = true;
            return validaciónCliente;
        }

        //Eliminar cliente (DELETE)
        public Validacion EliminarCliente(int dni)
        {
            Validacion validacion = new();
            List<ClienteDB> listaClientesDB = archivo.GetClienteDBList();
            ClienteDB cliente = listaClientesDB.FirstOrDefault(x => x.DNI == dni);
            if (cliente == null)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = $"El cliente de dni {dni} a eliminar no existe" });
                return validacion;
            }

            if (cliente.FechaEliminacion != null)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = $"El cliente de dni {dni} ya ha sido eliminado previamente" });
                validacion.Resultado = true;
                return validacion;
            }
            listaClientesDB.First(x => x.DNI == dni).FechaEliminacion = DateTime.Now;
            archivo.SaveClienteDB(listaClientesDB);
            validacion.Resultado = true;
            return validacion;
        }

        //Editar cliente (PUT)
        public ValidacionCliente EditarCliente(int dni, ClienteDto clienteModificado)
        {
            ValidacionCliente validCliente = new();
            List<ClienteDB> listaClientesDB = archivo.GetClienteDBList();
            var clienteAEditar = listaClientesDB.FirstOrDefault(u => u.DNI == dni);
            if (clienteAEditar == default || clienteAEditar.FechaEliminacion != null)
            {
                validCliente.Errores.Add(new Error() { ErrorDetail = "El cliente a editar no existe o fue eliminado" });
                return validCliente;
            }
            clienteModificado.DNI = dni;
            clienteAEditar = ModificarCliente(clienteModificado, clienteAEditar);
            listaClientesDB.RemoveAll(x => x.DNI == clienteAEditar.DNI);
            listaClientesDB.Add(clienteAEditar);
            listaClientesDB = listaClientesDB.OrderBy(x => x.FechaCreacion).ToList();
            archivo.SaveClienteDB(listaClientesDB);

            clienteModificado.DNI = clienteAEditar.DNI;
            validCliente.Cliente = clienteModificado;
            validCliente.Resultado = true;
            return validCliente;
        }

        //Obtener listado de clientes (GET)
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
                FechaNacimiento = X.FechaNacimiento
            }).ToList());
        }

        #region Auxiliares

        //Modificar valores del cliente guardado en archivo
        private static ClienteDB ModificarCliente(ClienteDto clienteMod, ClienteDB clienteAMod)
        {
            clienteAMod.Nombre = clienteMod.Nombre;
            clienteAMod.Apellido = clienteMod.Apellido;
            clienteAMod.Email = clienteMod.Email;
            clienteAMod.Telefono = clienteMod.Telefono;
            clienteAMod.LatitudGeografica = clienteMod.LatitudGeografica;
            clienteAMod.LongitudGeografica = clienteMod.LongitudGeografica;
            clienteAMod.FechaNacimiento = clienteMod.FechaNacimiento;
            clienteAMod.FechaActualizacion = DateTime.Now;
            return clienteAMod;
        }

        #endregion Auxiliares
    }
}