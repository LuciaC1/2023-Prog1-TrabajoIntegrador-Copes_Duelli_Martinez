namespace EmpresaEnvíoData
{
    public class ViajeDB
    {
        public int CodigoUnicoViaje { get; set; }
        public int Patente { get; set; }
        public DateTime FechaEntregasDesde { get; set; }
        public DateTime FechaEntregasHasta { get; set; }
        public int PorcentajeOcupacionCarga { get; set; }
        List<CompraDB> ListadoCompras { get; set;}
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

    }
}
