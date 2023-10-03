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
        public ProductoDB AñadirProducto(ProductoDto productoDto)
        {
            List<ProductoDB> listaProductos = archivo.GetProductoDBList();
            ProductoDB productoDB = new ProductoDB()
            {
                CodProducto=listaProductos.Count()+1,
                NombreProducto=productoDto.NombreProducto,
                MarcaProducto=productoDto.MarcaProducto,
                AltoCaja=productoDto.AltoCaja,
                AnchoCaja=productoDto.AnchoCaja,
                ProfundidadCaja=productoDto.ProfundidadCaja,
                PrecioUnitario=productoDto.PrecioUnitario,
                StockMinimo=productoDto.StockMinimo,
                StockTotal=productoDto.StockTotal,
                FechaCreacion=DateTime.Now,
            };
            listaProductos.Add(productoDB);
            archivo.SaveProductoDB(listaProductos);
            return productoDB;
        }
        public ProductoDB ActualizarStock(int codProducto,int stockNuevo)
        {
            List<ProductoDB> listaProductoDB = archivo.GetProductoDBList();
            if (listaProductoDB.Any(u => u.CodProducto == codProducto))
            {
                listaProductoDB.Find(u => u.CodProducto == codProducto).StockTotal = stockNuevo;
                listaProductoDB.Find(u => u.CodProducto == codProducto).FechaActualizacion = DateTime.Now;
                var productoEditado = listaProductoDB.Find(u => u.CodProducto == codProducto);

                return productoEditado;
            }
            return null;
        }
    }
}
