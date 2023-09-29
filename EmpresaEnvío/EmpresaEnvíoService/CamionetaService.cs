using EmpresaEnvíoData;
using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class CamionetaService
    {
        ArchivoCamioneta archivo;
        public CamionetaService()
        {
            archivo = new ArchivoCamioneta();
        }

        public List<CamionetaDto> ObtenerListadoCamionetas()
        {
            return (archivo.GetCamionetaDBList().Select(X => new CamionetaDto()
            {
                Nombre = X.Nombre,
                DistanciaMaxKM = X.DistanciaMaxKM,
                Patente = X.Patente,
                TamañoCargaCM3 = X.TamañoCargaCM3
            }).ToList());
        }
    }
}
