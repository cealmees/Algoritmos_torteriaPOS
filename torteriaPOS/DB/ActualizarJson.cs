using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using Windows.Storage;
using Windows.Data.Json;
using Newtonsoft.Json;
using System.Windows;

namespace torteriaPOS
{
    public class ActualizarJson
    {
        public CargarProductosJson nuevo = new CargarProductosJson();
        public ActualizarJson(CargarProductosJson aux)
        {
            nuevo = aux;
        }

        public async void crearJson()
        {
            Windows.Storage.StorageFolder fileFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile archivoPrueba = await fileFolder.CreateFileAsync("ingredientesTorta.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);

        }



        public async Task escribirJson()
        {
            crearJson();
            List<MostrarIngredientes> prueba = new List<MostrarIngredientes>();

            prueba.AddRange(nuevo.abarrotes);
            prueba.AddRange(nuevo.carniceria);
            prueba.AddRange(nuevo.cremeria);
            prueba.AddRange(nuevo.salchichoneria);

            string json = JsonConvert.SerializeObject(prueba.ToArray(), Formatting.Indented);

            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());

            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("ingredientesTorta.json");

            using (var s = await sampleFile.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }
        }
    }
}



