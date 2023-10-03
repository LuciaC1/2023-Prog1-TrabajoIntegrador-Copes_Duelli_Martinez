using EmpresaEnvíoData;
using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class ProductoService
    {
        ArchivoProducto archivo;
        public ProductoService()
        {
            archivo = new ArchivoProducto();
        }
        public ProductoDto AñadirProducto(ProductoDto productoDto)
        {
            List<ProductoDB> listaProductos = archivo.GetProductoDBList();
            ProductoDB productoDB = new ProductoDB()
            {
                Nombre
                DNI = clienteDto.DNI,
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                Email = clienteDto.Email,
                Telefono = clienteDto.Telefono,
                LatitudGeografica = clienteDto.LatitudGeografica,
                FechaNacimiento = clienteDto.FechaNacimiento,
                FechaCreacion = DateTime.Now,
            };
            listaClientes.Add(clienteDB);
            archivo.SaveClienteDB(listaClientes);
            return clienteDto;
        }
        public bool EliminarCliente(int dni)
        {
            List<ClienteDB> listaClientesDB = archivo.GetClienteDBList();
            ClienteDB cliente = listaClientesDB.FirstOrDefault(x => x.DNI == dni);
            if (cliente == null)
            {
                return false;
            }
            listaClientesDB.FirstOrDefault(x => x.DNI == dni).FechaEliminacion = DateTime.Now;
            archivo.SaveClienteDB(listaClientesDB);
            return true;
        }
        public ClienteDto EditarCliente(ClienteDto clienteModificado)
        {
            List<ClienteDB> listaClientesDB = archivo.GetClienteDBList();
            if (listaClientesDB.Any(u => u.DNI == clienteModificado.DNI))
            {
                var clienteAEditar = listaClientesDB.Find(u => u.DNI == clienteModificado.DNI);
                //Falta esto
                return clienteModificado;
            }
            return null;
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
                LatitudGeografica = X.LatitudGeografica
            }).ToList());
        }
    }
}
