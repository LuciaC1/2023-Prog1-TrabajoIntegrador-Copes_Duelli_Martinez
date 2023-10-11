namespace EmpresaEnvÍoDto
{
    public class ViajeDto
    {
        public int CodigoUnicoViaje { get; set; }
        public string Patente { get; set; }
        public DateTime FechaEntregasDesde { get; set; }
        public DateTime FechaEntregasHasta { get; set; }
        public double PorcentajeOcupacionCarga { get; set; }
        public List<int>? ListadoCompras { get; set; }
    }
}