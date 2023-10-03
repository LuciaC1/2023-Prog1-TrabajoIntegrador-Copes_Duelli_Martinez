using System.ComponentModel.DataAnnotations;

namespace EmpresaEnvÍoDto
{
    public class ClienteDto
    {
        [Required(ErrorMessage = "No se cargo un DNI")]
        public int DNI { get; set; }
        [Required(ErrorMessage = "No se cargo un Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "No se cargo un Apellido")]
        public string Apellido { get; set; }
        [EmailAddress(ErrorMessage = "No se cargo un Email valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "No se cargo un Telefono")]
        public int Telefono { get; set; }
        [Required(ErrorMessage = "No se cargo una latitud geográfica")]
        public float LatitudGeografica { get; set; }
        [Required(ErrorMessage = "No se cargo una longitud geográfica")]
        public float LongitudGeografica { get; set; }
        [Required(ErrorMessage = "No se cargo una fecha de nacimiento")]
        [DataType(DataType.DateTime, ErrorMessage = "No se cargo una fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }
}
