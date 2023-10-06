namespace EmpresaEnvÍoDto
{
    public class ViajeDto
    {
        public int CodigoUnicoViaje { get; set; }
        public int Patente { get; set; }
        public DateTime FechaEntregasDesde { get; set; }
        public DateTime FechaEntregasHasta { get; set; }
        public int PorcentajeOcupacionCarga { get; set; }
        public List<CompraDto>? ListadoCompras { get; set; }
    }
}
