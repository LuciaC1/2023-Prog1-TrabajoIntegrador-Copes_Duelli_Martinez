using Newtonsoft.Json;

namespace EmpresaEnvíoData
{
    public class ArchivoCompra
    {
        #region Constructor

        private string pathArchivo = "";

        public ArchivoCompra()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Archivos";
            Directory.CreateDirectory(filePath);
            pathArchivo = filePath + "\\compras.json";
        }

        #endregion Constructor

        #region Get CompraDB

        public List<CompraDB> GetCompraDBList()
        {
            List<CompraDB> listCompraDB = new List<CompraDB>();
            if (File.Exists(pathArchivo))
            {
                listCompraDB = JsonConvert.DeserializeObject<List<CompraDB>>(File.ReadAllText(pathArchivo));
            }
            return listCompraDB;
        }

        #endregion Get CompraDB

        #region Save CompraDB

        public void SaveCompraDB(List<CompraDB> listCompraDB)
        {
            string saveData = JsonConvert.SerializeObject(listCompraDB, Formatting.Indented);
            File.WriteAllText(pathArchivo, saveData);
        }

        #endregion Save CompraDB
    }
}