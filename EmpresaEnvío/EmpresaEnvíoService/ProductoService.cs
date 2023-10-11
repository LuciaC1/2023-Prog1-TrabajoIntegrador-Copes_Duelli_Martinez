using EmpresaEnvíoData;
using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class ProductoService
    {
        #region Constructor

        private ArchivoProducto archivo;

        public ProductoService()
        {
            archivo = new ArchivoProducto();
        }

        #endregion Constructor

        // Crear producto (POST)
        public ValidacionProducto AñadirProducto(ProductoDto productoDto)
        {
            Validacion validacion = productoDto.IsValid();
            ValidacionProducto validacionProducto = new()
            {
                Errores = validacion.Errores,
                Resultado = validacion.Resultado
            };
            if (validacionProducto.Errores.Count > 0)
            {
                return validacionProducto;
            }
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
                FechaCreacion = DateTime.Now
            };
            productoDto.CodProducto = productoDB.CodProducto;
            listaProductos.Add(productoDB);
            archivo.SaveProductoDB(listaProductos);
            validacionProducto.Resultado = true;
            validacionProducto.Producto = productoDto;
            return validacionProducto;
        }

        // Actualizar producto (PUT)
        public ValidacionProducto ActualizarStock(int codProducto, int stockNuevo)
        {
            ValidacionProducto validacion = new();
            List<ProductoDB> listaProductoDB = archivo.GetProductoDBList();
            ProductoDB producto = listaProductoDB.FirstOrDefault(u => u.CodProducto == codProducto);
            if (stockNuevo < 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El producto no puede tener un stock negativo" });
            }
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
            if (validacion.Errores.Count > 0)
            {
                return validacion;
            }
            listaProductoDB.Find(u => u.CodProducto == codProducto).StockTotal = stockNuevo;
            listaProductoDB.Find(u => u.CodProducto == codProducto).FechaActualizacion = DateTime.Now;
            var productoEditado = listaProductoDB.Find(u => u.CodProducto == codProducto);
            archivo.SaveProductoDB(listaProductoDB);
            var productoStockActualizado = new ProductoDto()
            {
                MarcaProducto = productoEditado.MarcaProducto,
                CodProducto = productoEditado.CodProducto,
                NombreProducto = productoEditado.NombreProducto,
                ProfundidadCaja = productoEditado.ProfundidadCaja,
                AnchoCaja = productoEditado.AnchoCaja,
                AltoCaja = productoEditado.AltoCaja,
                PrecioUnitario = productoEditado.PrecioUnitario,
                StockMinimo = productoEditado.StockMinimo,
                StockTotal = productoEditado.StockTotal
            };
            validacion.Producto = productoStockActualizado;
            validacion.Resultado = true;
            return validacion;
        }
    }
}