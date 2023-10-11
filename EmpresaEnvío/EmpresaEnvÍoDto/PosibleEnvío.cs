namespace EmpresaEnvÍoDto
{
    public class PosibleEnvío
    {
        public int CódigoCompra { get; set; }
        public string Patente { get; set; }
        public List<CompraDto> ListadoCompras { get; set; }
    }
}