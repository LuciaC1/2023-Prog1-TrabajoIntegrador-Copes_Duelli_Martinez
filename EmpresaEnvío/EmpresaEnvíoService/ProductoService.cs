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
                CodProducto = listaProductos.Count() + 1,
                NombreProducto = productoDto.NombreProducto,
                MarcaProducto = productoDto.MarcaProducto,
                AltoCaja = productoDto.AltoCaja,
                AnchoCaja = productoDto.AnchoCaja,
                ProfundidadCaja = productoDto.ProfundidadCaja,
                PrecioUnitario = productoDto.PrecioUnitario,
                StockMinimo = productoDto.StockMinimo,
                StockTotal = productoDto.StockTotal,
                FechaCreacion = DateTime.Now,
            };
            listaProductos.Add(productoDB);
            archivo.SaveProductoDB(listaProductos);
            return productoDto;
        }
        public Validacion ActualizarStock(int codProducto, int stockNuevo)
        {
            Validacion validacion = new Validacion();
            List<ProductoDB> listaProductoDB = archivo.GetProductoDBList();
            ProductoDB producto = listaProductoDB.FirstOrDefault(u => u.CodProducto == codProducto);
            if (producto == default)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El producto a actualizar stock no existe" });
                return validacion;
            }
            if (producto.FechaEliminacion != DateTime.MinValue)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El producto a actualizar stock ha sido eliminado" });
                return validacion;
            }

            if (stockNuevo < producto.StockMinimo)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = $@"El producto no puede tener un stock menor a su mínimo. 
Mínimo de stock: {producto.StockMinimo}" });
                return validacion;
            }
            listaProductoDB.Find(u => u.CodProducto == codProducto).StockTotal = stockNuevo;
            listaProductoDB.Find(u => u.CodProducto == codProducto).FechaActualizacion = DateTime.Now;
            var productoEditado = listaProductoDB.Find(u => u.CodProducto == codProducto);
            validacion.Resultado = true;
            return validacion;
        }
    }
}
