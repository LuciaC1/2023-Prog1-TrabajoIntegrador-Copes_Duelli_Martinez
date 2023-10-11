namespace EmpresaEnvÍoDto
{
    public class Validacion
    {
        public Validacion()
        {
            Errores = new List<Error>();
        }

        public bool Resultado { get; set; }

        public List<Error> Errores;
    }
}