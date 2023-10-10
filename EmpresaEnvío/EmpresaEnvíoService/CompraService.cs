using EmpresaEnvíoData;
using EmpresaEnvÍoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEnvíoService
{
    public class CompraService
    {
        ArchivoCompra archivoCompra;
        ArchivoProducto archivoProducto;
        public CompraService()
        {
            archivoCompra = new ArchivoCompra();
            archivoProducto = new ArchivoProducto();
        }
        //Metodo que da de alta una compra (registra una compra) y valida que haya stock del item a comprar.
        public CompraDto RegistrarCompra(CompraDto compra)
        {
            StockPrecioProducto stockPrecio = new StockPrecioProducto();
            stockPrecio = ValidarStockProducto(compra.CodigoProducto, compra.CantComprada);
            if (!stockPrecio.ResultadoStock)
            {
                throw new Exception("No hay suficiente stock del producto");
            }
            compra.MontoTotal = compra.CantComprada * stockPrecio.PrecioUnitario;
            compra.CalcularTotalDescuentoConIVA();
            List<CompraDB> listaComprasDB = archivoCompra.GetCompraDBList();
            CompraDB compraDB = new CompraDB()
            {
             CodigoProducto= listaComprasDB.Count +1,
             CantComprada= compra.CantComprada,
             CodigoCompra= compra.CodigoCompra,
             DNICliente= compra.DNICliente,
             EstadoCompra= EstadosCompraDB.OPEN,
             MontoTotal= compra.MontoTotal,
             FechaCreacion = DateTime.Now
            };
            listaComprasDB.Add(compraDB);
            archivoCompra.SaveCompraDB(listaComprasDB);
            return compra;
        }
        //Validacion del stock del producto
        private StockPrecioProducto ValidarStockProducto(int codigoProducto, int cantidadComprada)
        {
            List<ProductoDB> productos = archivoProducto.GetProductoDBList();
            StockPrecioProducto stockPrecioProducto = new StockPrecioProducto();
            var producto = productos.FirstOrDefault(p => p.CodProducto == codigoProducto);
            if (producto == null || (producto.StockTotal)-cantidadComprada < producto.StockMinimo)
            {
                stockPrecioProducto.ResultadoStock = false;
                return stockPrecioProducto;
            }
            stockPrecioProducto.PrecioUnitario = producto.PrecioUnitario;
            stockPrecioProducto.ResultadoStock = true;
            return stockPrecioProducto;
        }
    }
}
