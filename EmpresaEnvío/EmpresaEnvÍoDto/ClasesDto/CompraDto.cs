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
        public EstadosCompraDto EstadoCompra { get; set; }
        public double MontoTotal { get; set; }

        private double CalcularTotalConIVA()
        {
            const double IVA = 0.21;
            return MontoTotal + MontoTotal* IVA;
        }
        public void CalcularTotalDescuentoConIVA()
        {
            const double Descuento = 0.25;
            double totalConIVA = CalcularTotalConIVA();
            if (CantComprada > 4)
            {
                totalConIVA = totalConIVA + totalConIVA * Descuento;
            }
            this.MontoTotal=totalConIVA;
        }
    } 
}
