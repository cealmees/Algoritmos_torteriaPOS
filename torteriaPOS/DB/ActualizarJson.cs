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

        public async void crearJsonIngredientes()
        {
            Windows.Storage.StorageFolder fileFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile archivoPrueba = await fileFolder.CreateFileAsync("ingredientesTorta.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);

        }

        public async void crearJsonMenu()
        {
            Windows.Storage.StorageFolder fileFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile archivoPrueba = await fileFolder.CreateFileAsync("menuTorta.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);

        }

        public async Task escribirJsonIngredientes()
        {
            try
            {
                crearJsonIngredientes();

                string json = JsonConvert.SerializeObject(nuevo.MenuIngredientes.ToArray(), Formatting.Indented);

                byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());

                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("ingredientesTorta.json");

                using (var s = await sampleFile.OpenStreamForWriteAsync())
                {
                    s.Write(fileBytes, 0, fileBytes.Length);
                }
            }
            catch (FileLoadException) { }
            catch (FieldAccessException) { }
            
        }

        public async Task escribirJsonMenu()
        {
            try
            {
                crearJsonMenu();

                string json = JsonConvert.SerializeObject(nuevo.MenuTortas.ToArray(), Formatting.Indented);

                byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());

                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("menuTorta.json");

                using (var s = await sampleFile.OpenStreamForWriteAsync())
                {
                    s.Write(fileBytes, 0, fileBytes.Length);
                }
            }
            catch (FileLoadException) { }
            catch (FieldAccessException) { }

        }


    }

}



