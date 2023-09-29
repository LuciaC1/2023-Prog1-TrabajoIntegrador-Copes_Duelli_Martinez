using System.ComponentModel.DataAnnotations;

namespace EmpresaEnvÍoDto
{
    public class ViajeDto
    {
        [Required(ErrorMessage ="No se cargo un codigo de viaje")]
        public int CodigoUnicoViaje { get; set; }
        public int Patente { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaEntregasDesde { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaEntregasHasta { get; set; }
        [Required(ErrorMessage = "No se cargo un porcentaje de carga")]
        public int PorcentajeOcupacionCarga { get; set; }
        List<CompraDto>? ListadoCompras { get; set; }
    }
}
