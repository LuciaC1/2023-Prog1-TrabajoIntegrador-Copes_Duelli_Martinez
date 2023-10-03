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
        [Required(ErrorMessage = "No se cargo un Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "No se cargo un Telefono")]
        public int Telefono { get; set; }
        [Required(ErrorMessage = "No se cargo una latitud geografica")]
        public float LatitudGeografica { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaNacimiento { get; set; }
    }
}
