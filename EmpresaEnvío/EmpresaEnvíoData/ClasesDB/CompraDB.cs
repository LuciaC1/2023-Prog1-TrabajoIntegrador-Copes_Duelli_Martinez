using System.ComponentModel.DataAnnotations;

namespace EmpresaEnvíoData
{
    public class CompraDB
    {
        [Required(ErrorMessage ="No existe el codigo de compra")]
        public int CodigoCompra { get; set; }
        public int CodigoProducto { get; set; }
        public int DNICliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public int CantComprada { get; set; }
        public DateTime FechaEntregaSolicitada {get; set;}
        public EstadosCompraDB EstadoCompra { get; set; }
        public double MontoTotal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
