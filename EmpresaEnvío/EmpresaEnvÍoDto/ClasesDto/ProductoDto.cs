namespace EmpresaEnvÍoDto
{
    public class ProductoDto
    {
        public int CodProducto { get; set; }
        public string NombreProducto { get; set; }
        public string MarcaProducto { get; set; }
        public double AltoCaja { get; set; }
        public double AnchoCaja { get; set; }
        public double ProfundidadCaja { get; set; }
        public double PrecioUnitario { get; set; }
        public int StockMinimo { get; set; }
        public int StockTotal { get; set; }
        public Validacion IsValid()
        {
            Validacion validacion = new Validacion()
            {
                Errores = new List<Error>()
            };
            if (Double.IsNegative(AltoCaja))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La altura esta en numeros negativos" });
            }
            if (Double.IsNegative(AnchoCaja))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El ancho esta en numeros negativos" });
            }
            if (Double.IsNegative(ProfundidadCaja))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La profundidad esta en numeros negativos" });
            }
            if (Double.IsNegative(PrecioUnitario))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El precio esta en numeros negativos" });
            }
            return validacion;
        }
    }
}
