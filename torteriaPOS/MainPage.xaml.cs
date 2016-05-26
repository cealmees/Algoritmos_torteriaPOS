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
using System.Threading.Tasks;
using System.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace torteriaPOS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string rutaJsonLogIn = "ms-appx:///DB/login.Json";
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

                

                MySplitView.Visibility = Visibility.Visible;

                logInbtn.Visibility = Visibility.Collapsed;
                logOutbtn.Visibility = Visibility.Visible;
            }
                           
            else
                resultado.Visibility = Visibility.Visible;         

        }

        private void cancerlarFrm_Click(object sender, RoutedEventArgs e)
        {
            logInfrm.Visibility = Visibility.Collapsed;
        }

        CargarProductosJson listas = new CargarProductosJson();

        List<MostrarIngredientes> torta = new List<MostrarIngredientes>();
        private void mostrarIngredientes()
        {

            
            lvAbarrotes.ItemsSource = listas.abarrotes;
            lvCarniceria.ItemsSource = listas.carniceria;
            lvCremeria.ItemsSource = listas.cremeria;
            lvSalchichoneria.ItemsSource = listas.salchichoneria;
            lvVerduras.ItemsSource = listas.verduras;

            


            lvTorta.ItemsSource = torta;
        }

        
       


        public int auxCremeria;
        public int auxCarniceria;
        public int auxAbarrote;
        public int auxVerduras;
        public int auxSalchichoneria;
        public int auxTorta;

        public void lvCremeria_Tapped(object sender, TappedRoutedEventArgs e)
        {
            auxCremeria = lvCremeria.SelectedIndex;
        }

        private void nene_Click(object sender, RoutedEventArgs e)
        {
            listas.cremeria[auxCremeria].Cantidad -= 100;
            lvCremeria.ItemsSource = null;
            lvCremeria.ItemsSource = listas.cremeria;

        }

        private void btnHamburguesa_Click(object sender, RoutedEventArgs e)
        {
            //MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void logOutbtn_Click(object sender, RoutedEventArgs e)
        {
            await guardarDatos();
            Application.Current.Exit();
        }

        public async Task guardarDatos()
        {
            ActualizarJson prueba = new ActualizarJson(listas);
            await prueba.escribirJson();
        }

        
        private void Inventario_Click(object sender, RoutedEventArgs e)
        {
            bienvenidaUsuario.Text = "Inventario";
            bienvenidaUsuario.Visibility = Visibility.Visible;
            mostrarIngredientes();
            vistaIngredientes.Visibility = Visibility.Visible;
        }

        private void GetTortas_Click(object sender, RoutedEventArgs e)
        {
           //torta

            

            tortaDeMilanesaPollo();
            refrescarLv();
        }

        private void refrescarLv()
        {
            lvCremeria.ItemsSource = null;
            lvAbarrotes.ItemsSource = null;
            lvCarniceria.ItemsSource = null;
            lvVerduras.ItemsSource = null;
            lvSalchichoneria.ItemsSource = null;
            lvTorta.ItemsSource = null;

            lvCremeria.ItemsSource = listas.cremeria;
            lvAbarrotes.ItemsSource = listas.abarrotes;
            lvCarniceria.ItemsSource = listas.carniceria;
            lvVerduras.ItemsSource = listas.verduras;
            lvSalchichoneria.ItemsSource = listas.salchichoneria;
            lvTorta.ItemsSource = torta;
        }
        private void tortaDeMilanesaPollo()
        {
            //Digamos que esta torta de milanesa lleva:
            //milanesa Pollo = 100 gr
            //Jamon = 50 gr
            //quesillo = 70 gr
            //aguacate = 20 gr
            //mayonesa = 20 gr

            float[] tortaCantidad = new float[5] { 100, 50, 70, 20, 20 };

            listas.abarrotes[auxAbarrote].Cantidad -= tortaCantidad[4];
            listas.cremeria[auxCremeria].Cantidad -= tortaCantidad[2];
            listas.verduras[auxVerduras].Cantidad -= tortaCantidad[3];
            listas.salchichoneria[auxSalchichoneria].Cantidad -= tortaCantidad[1];
            listas.carniceria[auxCarniceria].Cantidad -= tortaCantidad[0];

            ArrayList contador = new ArrayList();

            

            foreach (MostrarIngredientes item01 in lvCremeria.SelectedItems)
            {
                torta.Add(item01);
            }
            foreach (MostrarIngredientes item01 in lvAbarrotes.SelectedItems)
            {
                torta.Add(item01);
            }
            foreach (MostrarIngredientes item01 in lvCarniceria.SelectedItems)
            {
                torta.Add(item01);
            }
            foreach (MostrarIngredientes item01 in lvVerduras.SelectedItems)
            {
                torta.Add(item01);
            }
            foreach (MostrarIngredientes item01 in lvSalchichoneria.SelectedItems)
            {
                torta.Add(item01);
            }

        }

        private void lvSalchichoneria_Tapped(object sender, TappedRoutedEventArgs e)
        {
            auxSalchichoneria = lvSalchichoneria.SelectedIndex;
        }

        private void lvCarniceria_Tapped(object sender, TappedRoutedEventArgs e)
        {
            auxCarniceria = lvCarniceria.SelectedIndex;
        }

        private void lvAbarrotes_Tapped(object sender, TappedRoutedEventArgs e)
        {
            auxAbarrote = lvAbarrotes.SelectedIndex;
        }

        private void lvVerduras_Tapped(object sender, TappedRoutedEventArgs e)
        {
            auxVerduras = lvVerduras.SelectedIndex;
        }

        private void lvTorta_Tapped(object sender, TappedRoutedEventArgs e)
        {
            auxTorta = lvTorta.SelectedIndex;
        }


    }
}
