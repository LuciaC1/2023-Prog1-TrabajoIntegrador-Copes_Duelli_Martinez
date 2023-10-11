using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class ResultadoEnvio
    {
        public string Patente { get; set; }

        public double PorcentajeOcupacion { get; set; }
        public List<CompraDto> ListadoCompras { get; set; }
    }
}