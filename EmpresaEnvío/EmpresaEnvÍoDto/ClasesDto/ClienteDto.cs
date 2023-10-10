namespace EmpresaEnvÍoDto
{
    public class ClienteDto
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public double LatitudGeografica { get; set; }
        public double LongitudGeografica { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Validacion IsValid()
        {
            Validacion validacion = new Validacion()
            {
                Errores = new List<Error>()
            };
            if (int.IsNegative(DNI))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El DNI no puede ser negativo" });
            }
            if (int.IsNegative(Telefono))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El telefono no puede ser negativo" });
            }
            return validacion;
        }
    }
}
