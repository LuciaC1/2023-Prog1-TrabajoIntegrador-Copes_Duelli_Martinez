
using Newtonsoft.Json;

namespace EmpresaEnvíoData
{
    public class ArchivoViaje
    {
        #region Constructor

        private string pathArchivo = "";

        public ArchivoViaje()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Files";
            Directory.CreateDirectory(filePath);
            pathArchivo = filePath + "\\Archivo.json";
        }

        #endregion Constructor

        #region Get ViajeDB

        public List<ViajeDB> GetViajeDBList()
        {
            List<ViajeDB> listViajeDB = new List<ViajeDB>();
            if (File.Exists(pathArchivo))
            {
                listViajeDB = JsonConvert.DeserializeObject<List<ViajeDB>>(File.ReadAllText(pathArchivo));
            }
            return listViajeDB;
        }

        #endregion Get ViajeDB

        #region Save ViajeDB

        public void SaveViajeDB(List<ViajeDB> listViajeDB)
        {
            string saveData = JsonConvert.SerializeObject(listViajeDB, Formatting.Indented);
            File.WriteAllText(pathArchivo, saveData);
        }

        #endregion Save ViajeDB

    }
}