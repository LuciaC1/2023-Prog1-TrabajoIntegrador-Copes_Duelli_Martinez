using EmpresaEnvÍoDto.ClasesDto;
using System.ComponentModel.DataAnnotations;

namespace EmpresaEnvÍoDto
{
    public class ProductoDto
    {
        [Required(ErrorMessage = "No se cargo el nombre del producto")]
        public string NombreProducto { get; set; }
        [Required(ErrorMessage = "No se cargo la marca del producto")]
        public string MarcaProducto { get; set; }
        [Required(ErrorMessage = "No se cargo el alto de la caja")]
        public double AltoCaja { get; set; }
        [Required(ErrorMessage = "No se cargo el ancho de la caja")]
        public double AnchoCaja { get; set; }
        [Required(ErrorMessage = "No se cargo la profundidad de la caja")]
        public double ProfundidadCaja { get; set; }
        [Required(ErrorMessage = "No se cargo el precio unitario")]
        public double PrecioUnitario { get; set; }
        [Required(ErrorMessage = "No se cargo un stock minimo")]
        public int StockMinimo { get; set; }
        [Required(ErrorMessage = "No se cargo un stock")]
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
