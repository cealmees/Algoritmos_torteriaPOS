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

        public List<RegistroEntradas> Registro = new List<RegistroEntradas>();

        public async void RegistroDiario()
        {
            try
            {

                Windows.Storage.StorageFolder Fol = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile Fil = await Fol.GetFileAsync("registroDiario.json");

                string nana = FileIO.ReadTextAsync(Fil).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                var Ser = new DataContractJsonSerializer(typeof(RegistroEntradas));
                JsonArray an = JsonArray.Parse(nana);
                foreach (JsonValue oJsonVal in an)
                {
                    JsonObject oJsonObj = oJsonVal.GetObject();
                    using (MemoryStream jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(oJsonObj.ToString())))
                    {
                        RegistroEntradas oContent = (RegistroEntradas)Ser.ReadObject(jsonStream);
                        Registro.Add(oContent);

                    }
                }

            }
            catch (Exception)
            {
            }
        }



    }
}



