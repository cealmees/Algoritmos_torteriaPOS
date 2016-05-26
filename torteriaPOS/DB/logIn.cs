using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Windows.Data.Json;
using Windows.Storage;
using System.Runtime.Serialization.Json;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;

namespace torteriaPOS
{
    class LogIn
    {
        //Variables globales de encapsulamiento privado

        private string json;
        private string userName;
        private string userPassword;
        public string usuarioActivo;

        //Constructor de clase
        public LogIn(string jsonRuta, string usuario, string clave)
        {
            json = jsonRuta;
            userName = usuario;
            userPassword = clave;
        }

        public bool autenticarUsuario()
        {
            return autenticar();
        }
        private bool autenticar()
        {

            bool verificar = false;

            try
            {
                //Cargamos la ruta del Json
                Uri rutaApp = new Uri(json);
                //Creamos la ruta al amacenamiento
                StorageFile logFile = StorageFile.GetFileFromApplicationUriAsync(rutaApp).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                //Cargamos el contenido en formato Json a una String nueva
                string contenidoJson = FileIO.ReadTextAsync(logFile).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                var jsonSerializador = new DataContractJsonSerializer(typeof(LoginSettings));
                JsonArray logArray = JsonArray.Parse(contenidoJson);
                ArrayList userArray = new ArrayList();
                ArrayList passwordArray = new ArrayList();
                ArrayList nameArray = new ArrayList();

                foreach (JsonValue valorJson in logArray)
                {
                    JsonObject objJson = valorJson.GetObject();
                    using (MemoryStream jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(objJson.ToString())))
                    {
                        LoginSettings valores = (LoginSettings)jsonSerializador.ReadObject(jsonStream);
                        userArray.Add(valores.user);
                        passwordArray.Add(valores.password);
                        nameArray.Add(valores.nombre);
                    }
                }
                //Ciclo para crear el login
                for (int i = 0; i < userArray.Count; i++)
                {
                    if (userName == userArray[i].ToString())
                    {
                        if (userPassword == passwordArray[i].ToString())
                        {
                            verificar = true;
                            usuarioActivo = nameArray[i].ToString();
                            break;
                        }
                    }
                    else
                        verificar = false;

                }

            }
            catch (Exception) { }

            return verificar;

        }



    }
}
