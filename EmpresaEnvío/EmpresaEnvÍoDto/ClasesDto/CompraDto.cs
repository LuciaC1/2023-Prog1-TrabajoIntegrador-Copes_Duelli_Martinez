namespace EmpresaEnvÍoDto
{
    public class CompraDto
    {
        public int CodigoCompra { get; set; }
        public int CodigoProducto { get; set; }
        public int DNICliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public int CantComprada { get; set; }
        public DateTime FechaEntregaSolicitada { get; set; }
        public EstadosCompra EstadoCompra { get; set; }
        public int MontoTotal { get; set; }

        const decimal IVA = 0.21M;
        const decimal Descuento = 0.25M; 

        private decimal CalcularTotalConIVA()
        {
            return MontoTotal + MontoTotal* IVA;
        }
        private decimal CalcularTotalDescuentoConIVA()
        {
            decimal totalConIVA = CalcularTotalConIVA();
            if (CantComprada > 4)
            {
                return totalConIVA + totalConIVA * Descuento;
            }
            return totalConIVA;
        }
    } 
}
