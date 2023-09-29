
using Newtonsoft.Json;

namespace EmpresaEnvíoData
{
    public class ArchivoProducto
    {
        #region Constructor

        private string pathArchivo = "";

        public ArchivoProducto()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Files";
            Directory.CreateDirectory(filePath);
            pathArchivo = filePath + "\\productos.json";
        }

        #endregion Constructor

        #region Get ProductoDB

        public List<ProductoDB> GetProductoDBList()
        {
            List<ProductoDB> listProductoDB = new List<ProductoDB>();
            if (File.Exists(pathArchivo))
            {
                listProductoDB = JsonConvert.DeserializeObject<List<ProductoDB>>(File.ReadAllText(pathArchivo));
            }
            return listProductoDB;
        }

        #endregion Get ProductoDB

        #region Save ProductoDB

        public void SaveProductoDB(List<ProductoDB> listProductoDB)
        {
            string saveData = JsonConvert.SerializeObject(listProductoDB, Formatting.Indented);
            File.WriteAllText(pathArchivo, saveData);
        }

        #endregion Save ProductoDB
    }
}