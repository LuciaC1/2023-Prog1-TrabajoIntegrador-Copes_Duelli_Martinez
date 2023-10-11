using Newtonsoft.Json;

namespace EmpresaEnvíoData
{
    public class ArchivoCamioneta
    {
        #region Constructor

        private string pathArchivo = "";

        public ArchivoCamioneta()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Archivos";
            Directory.CreateDirectory(filePath);
            pathArchivo = filePath + "\\camionetas.json";
        }

        #endregion Constructor

        #region Get CamionetaDB

        public List<Camioneta> GetCamionetaDBList()
        {
            List<Camioneta> listCamionetaDB = new List<Camioneta>();
            if (File.Exists(pathArchivo))
            {
                listCamionetaDB = JsonConvert.DeserializeObject<List<Camioneta>>(File.ReadAllText(pathArchivo));
            }
            return listCamionetaDB;
        }

        #endregion Get CamionetaDB

        #region Save CamionetaDB

        public void SaveCamionetaDB(List<Camioneta> listCamionetaDB)
        {
            string saveData = JsonConvert.SerializeObject(listCamionetaDB, Formatting.Indented);
            File.WriteAllText(pathArchivo, saveData);
        }

        #endregion Save CamionetaDB
    }
}