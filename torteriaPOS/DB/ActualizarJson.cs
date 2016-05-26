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


        public async Task WriteToFile()
        {
            string json = JsonConvert.SerializeObject(nuevo.cremeria.ToArray());

            // Get the text data from the textbox. 
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder ;

            // Create a new folder name DataFolder.

            var dataFolder = await local.CreateFolderAsync("DB",
                CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            var file = await dataFolder.CreateFileAsync("Ingredientes.txt", CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }
        }
    }
}


//        public async void actJson()
//        {

//            UnicodeEncoding uniencoding = new UnicodeEncoding();
//            //System.IO.File.WriteAllText("ms-appx:///DB/Ingredientes.json", json);
//                byte[] resultado = uniencoding.GetBytes(json);

//                using (FileStream JsonFile = File.Open(@"ms-appx:///DB/Ingredientes.json", FileMode.OpenOrCreate))
//                {
//                    JsonFile.Seek(0, SeekOrigin.End);

//                    await JsonFile.WriteAsync(resultado, 0, resultado.Length);
//                }


//            //File.WriteAllText(@"ms-appx:///DB/Ingredientes.json", JsonConvert.SerializeObject(nuevo.cremeria));
//            //try
//            //{
//            //    string json = JsonConvert.SerializeObject(nuevo.cremeria.ToArray());
//            //    System.IO.File.WriteAllText(@"ms-appx:///DB/Ingredientes.json", json);
//            //    //using (StreamWriter file = File.CreateText(@"ms-appx:///DB/Ingredientes.json"))
//            //    //{
//            //    //    JsonSerializer serializador = new JsonSerializer();
//            //    //    serializador.Serialize(file, nuevo.cremeria);
//            //    //}
//            //}
//            //catch (Exception exp)
//            //{
//            //}
//        }
//    }
//}
