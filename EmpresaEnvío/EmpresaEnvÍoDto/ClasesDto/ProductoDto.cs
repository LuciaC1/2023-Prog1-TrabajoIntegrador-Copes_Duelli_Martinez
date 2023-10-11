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
            Validacion validacion = new()
            {
                Errores = new List<Error>()
            };
            if (AltoCaja <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La altura debe ser mayor a cero" });
            }
            if (AnchoCaja <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El ancho debe ser mayor a cero" });
            }
            if (ProfundidadCaja <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La profundidad debe ser mayor a cero" });
            }
            if (PrecioUnitario <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El precio debe ser mayor a cero" });
            }
            if (StockMinimo <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El stock mínimo debe ser mayor a cero" });
            }
            if (int.IsNegative(StockTotal))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El stock total no puede ser negativo" });
            }
            if (validacion.Errores.Count == 0) validacion.Resultado = true;
            return validacion;
        }
    }
}