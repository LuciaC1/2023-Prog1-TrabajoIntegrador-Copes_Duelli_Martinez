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

        public List<CamionetaDB> GetCamionetaDBList()
        {
            List<CamionetaDB> listCamionetaDB = new List<CamionetaDB>();
            if (File.Exists(pathArchivo))
            {
                listCamionetaDB = JsonConvert.DeserializeObject<List<CamionetaDB>>(File.ReadAllText(pathArchivo));
            }
            return listCamionetaDB;
        }

        #endregion Get CamionetaDB

        #region Save CamionetaDB

        public void SaveCamionetaDB(List<CamionetaDB> listCamionetaDB)
        {
            string saveData = JsonConvert.SerializeObject(listCamionetaDB, Formatting.Indented);
            File.WriteAllText(pathArchivo, saveData);
        }

        #endregion Save CamionetaDB
    }
}
