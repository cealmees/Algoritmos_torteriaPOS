using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using Windows.Storage;
using Windows.Data.Json;

namespace torteriaPOS
{
    public class CargarProductosJson
    {
        public CargarProductosJson()
        {
            leerMenuIngredientes();
            leerMenuTortas();
        }
        public List<IngredientesCategoria> MenuIngredientes = new List<IngredientesCategoria>();

        public async void leerMenuIngredientes()
        {
            try
            {

                Windows.Storage.StorageFolder Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile JSONINGREDIENTES = await Folder.GetFileAsync("ingredientesTorta.json");

                string jText = FileIO.ReadTextAsync(JSONINGREDIENTES).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                var jsonSerializer = new DataContractJsonSerializer(typeof(IngredientesCategoria));
                JsonArray anjarray = JsonArray.Parse(jText);
                foreach (JsonValue oJsonVal in anjarray)
                {
                    JsonObject oJsonObj = oJsonVal.GetObject();
                    using (MemoryStream jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(oJsonObj.ToString())))
                    {
                        IngredientesCategoria oContent = (IngredientesCategoria)jsonSerializer.ReadObject(jsonStream);
                        MenuIngredientes.Add(oContent);
                    }
                }
               
            }
            catch (Exception)
            {
            }
        }

        public List<TortasCreador> MenuTortas = new List<TortasCreador>();

        public async void leerMenuTortas()
        {
            try
            {

                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("menuTorta.json");

                string jsonText = FileIO.ReadTextAsync(sampleFile).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                var jsonSerializer = new DataContractJsonSerializer(typeof(TortasCreador));
                JsonArray anjarray = JsonArray.Parse(jsonText);
                foreach (JsonValue oJsonVal in anjarray)
                {
                    JsonObject oJsonObj = oJsonVal.GetObject();
                    using (MemoryStream jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(oJsonObj.ToString())))
                    {
                        TortasCreador oContent = (TortasCreador)jsonSerializer.ReadObject(jsonStream);
                        MenuTortas.Add(oContent);

                    }
                }

            }
            catch (Exception)
            {
            }
        }





    }
}
