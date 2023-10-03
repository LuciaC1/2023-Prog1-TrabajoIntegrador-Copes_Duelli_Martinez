using EmpresaEnvÍoDto.ClasesDto;

namespace EmpresaEnvÍoDto
{
    public class ProductoDto
    {
        public string NombreProducto { get; set; }
        public string MarcaProducto { get; set; }
        public double AltoCaja { get; set; }
        public double AnchoCaja { get; set; }
        public double ProfundidadCaja { get; set; }
        public double PrecioUnitario { get; set; }
        public int StockMinimo { get; set; }
        public int StockTotal { get; set; }
        public ValidacionDto IsValid()
        {
            ValidacionDto validacion = new ValidacionDto()
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
            return validacion;
        }
    }
}
