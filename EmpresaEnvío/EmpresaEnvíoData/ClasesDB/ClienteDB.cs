namespace EmpresaEnvíoData
{
    public class ClienteDB
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public float LatitudGeografica { get; set; }
        public DateTime FehaNacimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
