using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Windows.Storage.Streams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace torteriaPOS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string rutaJsonLogIn = "ms-appx:///DB/login.Json";
        public string rutaJsonProductos = "ms-appx:///DB/Productos.json";
        public MainPage()
        {
            this.InitializeComponent();

        }

        private void logIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void logInbtn_Click(object sender, RoutedEventArgs e)
        {
            logInfrm.Visibility = Visibility.Visible;
        }

        private void enviarFrm_Click(object sender, RoutedEventArgs e)
        {
            LogIn accesoUsuario = new LogIn(rutaJsonLogIn, userFrm.Text, passwordFrm.Password);
            bool aux = accesoUsuario.autenticarUsuario();
            if (aux == true)
            {
                logInfrm.Visibility = Visibility.Collapsed;
                bienvenidaUsuario.Text = "¡Bienvenido "+ accesoUsuario.usuarioActivo+"!";
                bienvenidaUsuario.Visibility = Visibility.Visible;
                mostrarIngredientes();

                vistaIngredientes.Visibility = Visibility.Visible;

                MySplitView.Visibility = Visibility.Visible;
            }
                           
            else
                resultado.Visibility = Visibility.Visible;         

        }

        private void cancerlarFrm_Click(object sender, RoutedEventArgs e)
        {
            logInfrm.Visibility = Visibility.Collapsed;
        }

        CargarProductosJson listas = new CargarProductosJson();

        private void mostrarIngredientes()
        {

            
            lvAbarrotes.ItemsSource = listas.abarrotes;
            lvCarniceria.ItemsSource = listas.carniceria;
            lvCremeria.ItemsSource = listas.cremeria;
            lvSalchichoneria.ItemsSource = listas.salchichoneria;
            lvVerduras.ItemsSource = listas.verduras;


        }




        public int aux;
        public void lvCremeria_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int auxTest = lvCremeria.SelectedIndex;
            aux = auxTest;
            //prueba.Text = aux.ToString();
        }

        private  void nene_Click(object sender, RoutedEventArgs e)
        {
            listas.cremeria[aux].Cantidad -= 100;

            //try
            //{
            //    //prueba.Text = listas.cremeria[aux].Cantidad.ToString();

            //    string pruebaAuxSer = JsonConvert.SerializeObject(listas.cremeria.ToArray());
            //    string fileName = "ms-appx:///DB/Ingredientes.json";
            //    Uri appUri = new Uri(fileName);
            //    StorageFile file = StorageFile.GetFileFromApplicationUriAsync(appUri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();

            //    using (IRandomAccessStream textStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            //    {
            //        // write the JSON string!
            //        using (DataWriter textWriter = new DataWriter(textStream))
            //        {
            //            textWriter.WriteString(pruebaAuxSer);
            //            await textWriter.StoreAsync();
            //        }
            //    }
            //}
            //catch (FileNotFoundException) { }

            lvCremeria.ItemsSource = null;
            lvCremeria.ItemsSource = listas.cremeria;

        }

        private void btnHamburguesa_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
    }
}
