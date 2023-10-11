namespace EmpresaEnvíoData
{
    public class ViajeDB
    {
        public int CodigoUnicoViaje { get; set; }
        public string Patente { get; set; }
        public DateTime FechaEntregasDesde { get; set; }
        public DateTime FechaEntregasHasta { get; set; }
        public double PorcentajeOcupacionCarga { get; set; }
        public List<int>? ListadoCompras { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}