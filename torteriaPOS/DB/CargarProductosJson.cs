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
    class CargarProductosJson
    {
        public List<MostrarIngredientes> cremeria = new List<MostrarIngredientes>();
        public List<MostrarIngredientes> salchichoneria = new List<MostrarIngredientes>();
        public List<MostrarIngredientes> carniceria = new List<MostrarIngredientes>();
        public List<MostrarIngredientes> abarrotes = new List<MostrarIngredientes>();
        public List<MostrarIngredientes> verduras = new List<MostrarIngredientes>();

        public CargarProductosJson()
        {
            ReadFromDefaultFile();
        }
        public void ReadFromDefaultFile()
        {
            try
            {
                string fileName = "ms-appx:///DB/Ingredientes.json";
                Uri appUri = new Uri(fileName);//File name should be prefixed with 'ms-appx:///Assets/*
                StorageFile anjFile = StorageFile.GetFileFromApplicationUriAsync(appUri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                string jsonText = FileIO.ReadTextAsync(anjFile).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                var jsonSerializer = new DataContractJsonSerializer(typeof(MostrarIngredientes));
                JsonArray anjarray = JsonArray.Parse(jsonText);
                foreach (JsonValue oJsonVal in anjarray)
                {
                    JsonObject oJsonObj = oJsonVal.GetObject();
                    using (MemoryStream jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(oJsonObj.ToString())))
                    {
                        MostrarIngredientes oContent = (MostrarIngredientes)jsonSerializer.ReadObject(jsonStream);
                        if (oContent.Categoria == "Cremeria")
                            cremeria.Add(oContent);
                        if (oContent.Categoria == "Salchichoneria")
                            salchichoneria.Add(oContent);
                        if (oContent.Categoria == "Carniceria")
                            carniceria.Add(oContent);
                        if (oContent.Categoria == "Abarrotes")
                            abarrotes.Add(oContent);
                        if (oContent.Categoria == "Verduras")
                            verduras.Add(oContent);
                    }
                }
            }
            catch (Exception exp)
            {
            }
        }
    }
}
