namespace EmpresaEnvÍoDto
{
    public class CompraDto
    {
        public int CodigoCompra { get; set; }
        public int CodigoProducto { get; set; }
        public int DNICliente { get; set; }
        public DateTime FechaCompra { get; set; }
        public int CantComprada { get; set; }
        public DateTime FechaEntregaSolicitada { get; set; }
        public EstadosCompraDto EstadoCompra { get; set; }
        public double LatitudGeografica { get; set; }
        public double LongitudGeografica { get; set; }
        public double MontoTotal { get; set; }

        private double CalcularTotalConIVA()
        {
            const double IVA = 0.21;
            return MontoTotal + MontoTotal * IVA;
        }

        public void CalcularTotalDescuentoConIVA()
        {
            const double Descuento = 0.25;
            double totalConIVA = CalcularTotalConIVA();
            if (CantComprada > 4)
            {
                totalConIVA += totalConIVA * Descuento;
            }
            MontoTotal = totalConIVA;
        }

        public Validacion IsValid()
        {
            Validacion validacion = new Validacion();
            if (CodigoProducto <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El código de producto debe ser mayor a cero" });
            }
            if (DNICliente <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El DNI del cliente debe ser mayor a cero" });
            }
            if (CantComprada <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La cantidad comprada debe ser mayor a cero" });
            }
            if (FechaEntregaSolicitada < DateTime.Now)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La fecha de entrega solicitada debe ser mayor a la fecha actual" });
            }
            if (validacion.Errores.Count > 0)
            {
                return validacion;
            }
            validacion.Resultado = true;
            return validacion;
        }
    }
}