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
        public List<CompraDB> RegistrarCompra(CompraDto compra)
        {
            if (!ValidarStockProducto(compra.CodigoProducto, compra.CantComprada))
            {
                throw new Exception("No hay suficiente stock del producto");
            }
            List<CompraDB> listaComprasDB = archivoCompra.GetCompraDBList();
            CompraDB compraDB = new CompraDB()
            {
             CodigoProducto= listaComprasDB.Count +1,
             CantComprada= compra.CantComprada,
             CodigoCompra= compra.CodigoCompra,
             DNICliente= compra.DNICliente,
             EstadoCompra= (EmpresaEnvíoData.EstadosCompra)compra.EstadoCompra,
             MontoTotal= compra.MontoTotal,
             FechaCreacion = DateTime.Now
            };
            listaComprasDB.Add(compraDB);
            archivoCompra.SaveCompraDB(listaComprasDB);
            return listaComprasDB;
        }
        //Validacion del stock del producto
        private bool ValidarStockProducto(int codigoProducto, int cantidadComprada)
        {
            List<ProductoDB> productos = archivoProducto.GetProductoDBList();
            var producto = productos.FirstOrDefault(p => p.CodProducto == codigoProducto);
            if (producto == null || (producto.StockTotal)-cantidadComprada < producto.StockMinimo)
            {
                return false;
            }
            return true;
        }
    }
}
