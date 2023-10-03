
using Newtonsoft.Json;

namespace EmpresaEnvíoData
{
    public class ArchivoCliente
    {
        #region Constructor

        private string pathArchivo = "";

        public ArchivoCliente()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Archivos";
            Directory.CreateDirectory(filePath);
            pathArchivo = filePath + "\\clientes.json";
        }

        #endregion Constructor

        #region Get ClienteDB

        public List<ClienteDB> GetClienteDBList()
        {
            List<ClienteDB> listClienteDB = new List<ClienteDB>();
            if (File.Exists(pathArchivo))
            {
                listClienteDB = JsonConvert.DeserializeObject<List<ClienteDB>>(File.ReadAllText(pathArchivo));
            }
            return listClienteDB;
        }

        #endregion Get ClienteDB

        #region Save ClienteDB

        public void SaveClienteDB(List<ClienteDB> listClienteDB)
        {
            string saveData = JsonConvert.SerializeObject(listClienteDB, Formatting.Indented);
            File.WriteAllText(pathArchivo, saveData);
        }

        #endregion Save ClienteDB
    }
}
