using EmpresaEnvíoData;

namespace EmpresaEnvíoService
{
    public class CamionetaService
    {
        public static List<Camioneta> ObtenerListadoCamionetas()
        {
            return new ArchivoCamioneta().GetCamionetaDBList();
        }
    }
}