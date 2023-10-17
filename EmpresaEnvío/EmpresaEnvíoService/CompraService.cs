using EmpresaEnvíoData;
using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class CompraService
    {
        #region Constructor

        private ArchivoCompra archivoCompra;
        private ArchivoProducto archivoProducto;

        public CompraService()
        {
            archivoCompra = new ArchivoCompra();
            archivoProducto = new ArchivoProducto();
        }

        #endregion Constructor

        //Metodo que da de alta una compra (POST)
        public ValidacionCompra RegistrarCompra(CompraDto compra)
        {
            ValidacionCompra validacionCompra = new();
            ProductoDB producto = archivoProducto.GetProductoDBList().FirstOrDefault(p => p.CodProducto == compra.CodigoProducto);
            if (producto == default)
            {
                validacionCompra.Errores.Add(new Error() { ErrorDetail = "El producto a comprar no existe" });
                return validacionCompra;
            }
            StockPrecioProducto stockPrecio = ValidarStockProducto(producto, compra.CantComprada);
            if (!stockPrecio.ResultadoStock)
            {
                validacionCompra.Errores.Add(new Error() { ErrorDetail = "No hay stock del producto suficiente" });
                return validacionCompra;
            }
            ClienteDB cliente = new ArchivoCliente().GetClienteDBList().FirstOrDefault(c => c.DNI == compra.DNICliente);
            if (cliente == default)
            {
                validacionCompra.Errores.Add(new Error() { ErrorDetail = "El cliente no existe" });
                return validacionCompra;
            }
            if (cliente.FechaEliminacion != null)
            {
                validacionCompra.Errores.Add(new Error() { ErrorDetail = "El cliente ha sido eliminado previamente" });
                return validacionCompra;
            }
            compra.LatitudGeografica = cliente.LatitudGeografica;
            compra.LongitudGeografica = cliente.LongitudGeografica;
            compra.MontoTotal = compra.CantComprada * stockPrecio.PrecioUnitario;
            compra.CalcularTotalDescuentoConIVA();
            List<CompraDB> listaComprasDB = archivoCompra.GetCompraDBList();
            CompraDB compraDB = new()
            {
                CodigoProducto = compra.CodigoProducto,
                CantComprada = compra.CantComprada,
                CodigoCompra = listaComprasDB.Count + 1,
                DNICliente = compra.DNICliente,
                EstadoCompra = EstadosCompraDB.OPEN,
                LatitudGeografica = compra.LatitudGeografica,
                LongitudGeografica = compra.LongitudGeografica,
                MontoTotal = compra.MontoTotal,
                FechaEntregaSolicitada = compra.FechaEntregaSolicitada,
                FechaCreacion = DateTime.Now,
                FechaCompra = DateTime.Now
            };
            listaComprasDB.Add(compraDB);
            archivoCompra.SaveCompraDB(listaComprasDB);
            validacionCompra.Resultado = true;
            compra.CodigoCompra = compraDB.CodigoCompra;
            compra.FechaCompra = compraDB.FechaCompra;
            compra.EstadoCompra = (EstadosCompraDto)compraDB.EstadoCompra;
            validacionCompra.Compra = compra;
            List<ProductoDB> listaProductosDB = archivoProducto.GetProductoDBList();
            listaProductosDB.First(x => x.CodProducto == compra.CodigoProducto).StockTotal -= compra.CantComprada;
            archivoProducto.SaveProductoDB(listaProductosDB);
            return validacionCompra;
        }

        #region Auxiliares

        //Validacion del stock del producto
        private static StockPrecioProducto ValidarStockProducto(ProductoDB producto, int cantidadComprada)
        {
            StockPrecioProducto stockPrecioProducto = new();
            if (producto.StockTotal - cantidadComprada < 0 || producto.StockTotal < producto.StockMinimo)
            {
                stockPrecioProducto.ResultadoStock = false;
                return stockPrecioProducto;
            }
            stockPrecioProducto.PrecioUnitario = producto.PrecioUnitario;
            stockPrecioProducto.ResultadoStock = true;
            return stockPrecioProducto;
        }

        public List<CompraDto> GetComprasDtoOpen(DateTime fechaDesde, DateTime fechaHasta)
        {
            return archivoCompra.GetCompraDBList().
                Where(x => x.EstadoCompra == EstadosCompraDB.OPEN && x.FechaEntregaSolicitada > fechaDesde && x.FechaEntregaSolicitada < fechaHasta)
                .Select(x => new CompraDto()
                {
                    CodigoProducto = x.CodigoProducto,
                    DNICliente = x.DNICliente,
                    FechaEntregaSolicitada = x.FechaEntregaSolicitada,
                    CodigoCompra = x.CodigoCompra,
                    CantComprada = x.CantComprada,
                    EstadoCompra = EstadosCompraDto.OPEN,
                    LatitudGeografica = x.LatitudGeografica,
                    LongitudGeografica = x.LongitudGeografica,
                    MontoTotal = x.MontoTotal,
                    FechaCompra = x.FechaCompra
                }).ToList();
        }

        #endregion Auxiliares
    }
}