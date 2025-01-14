using DataClasses.Core;
using System.Text.Json;

namespace PruebaWebApi.Utils
{
    public class FileHelper
    {
        private const string FilePath = "products.json";

        public static List<ProductCore> LoadProducts()
        {
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]"); // Crear un archivo vacío si no existe
            }

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<ProductCore>>(json) ?? new List<ProductCore>();
        }

        public static void SaveProducts(List<ProductCore> products)
        {
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
