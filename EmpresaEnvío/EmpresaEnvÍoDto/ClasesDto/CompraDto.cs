using System.ComponentModel.DataAnnotations;

namespace EmpresaEnvÍoDto
{
    public class CompraDto
    {
        [Required(ErrorMessage = "No se cargo un codigo de compra")]
        public int CodigoCompra { get; set; }
        [Required(ErrorMessage = "No se cargo un codigo de producto")]
        public int CodigoProducto { get; set; }
        [Required(ErrorMessage ="No se cargo el DNI del cliente")]
        public int DNICliente { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaVenta { get; set; }
        [Required(ErrorMessage = "No se cargo la cantidad de producto a comprar")]
        public int CantComprada { get; set; }
        public DateTime FechaEntregaSolicitada { get; set; }
        [DataType(DataType.DateTime)]
        public EstadosCompra EstadoCompra { get; set; }
        [Required(ErrorMessage = "No se cargo un monto total de la compra")]
        public int MontoTotal { get; set; }
    }
}
