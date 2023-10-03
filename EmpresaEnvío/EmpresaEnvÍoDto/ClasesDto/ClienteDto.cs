namespace EmpresaEnvÍoDto
{
    public class ClienteDto
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public float LatitudGeografica { get; set; }
        public float LongitudGeografica { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
