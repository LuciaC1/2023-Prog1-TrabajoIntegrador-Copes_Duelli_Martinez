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
            if (DNI <= 0)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El DNI no es válido" });
            }
            if (int.IsNegative(Telefono))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "El teléfono no puede ser negativo" });
            }
            if (LongitudGeografica is < -180 or > 180)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La longitud ingresada se encuentra fuera del rango posible" });
            }
            if (LatitudGeografica is < -90 or > 90)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La latitud ingresada se encuentra fuera del rango posible" });
            }
            if (FechaNacimiento > DateTime.Now)
            {
                validacion.Errores.Add(new Error { ErrorDetail = "La fecha de nacimiento no puede ser mayor o igual a la fecha actual" });
            }
            if (validacion.Errores.Count == 0) validacion.Resultado = true;
            return validacion;
        }
    }
}