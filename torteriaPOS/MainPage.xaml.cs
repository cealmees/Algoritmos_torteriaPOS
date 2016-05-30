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
        

        List<Productos> torta = new List<Productos>();
        private void mostrarIngredientes()
        {


            lvAbarrotes.ItemsSource = listas.MenuIngredientes[0].SubProductos;
            lvCarniceria.ItemsSource = listas.MenuIngredientes[1].SubProductos;
            lvCremeria.ItemsSource = listas.MenuIngredientes[2].SubProductos;
            lvSalchichoneria.ItemsSource = listas.MenuIngredientes[3].SubProductos;
            lvVerduras.ItemsSource = listas.MenuIngredientes[4].SubProductos;




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

        //private void nene_Click(object sender, RoutedEventArgs e)
        //{
        //    listas.cremeria[auxCremeria].Cantidad -= 100;
        //    listas.MenuIngredientes[0].SubProductos[0].Precio = 1000;
        //    lvCremeria.ItemsSource = null;
        //    lvCremeria.ItemsSource = listas.cremeria;

        //}

        private void btnHamburguesa_Click(object sender, RoutedEventArgs e)
        {
            //MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void logOutbtn_Click(object sender, RoutedEventArgs e)
        {

            ActualizarJson MenuTortas = new ActualizarJson(listas);
            await MenuTortas.escribirJsonMenu();
            await guardarDatos();
            Application.Current.Exit();
        }

        public async Task guardarDatos()
        {
            ActualizarJson Datos = new ActualizarJson(listas);
            await Datos.escribirJsonIngredientes();
            await Datos.escribirJsonMenu();
        }


        private void Inventario_Click(object sender, RoutedEventArgs e)
        {
            bienvenidaUsuario.Text = "Inventario";
            bienvenidaUsuario.Visibility = Visibility.Visible;
            mostrarIngredientes();
            vistaIngredientes.Visibility = Visibility.Visible;
        }

        //private void GetTortas_Click(object sender, RoutedEventArgs e)
        //{
        //    //torta



        //    tortaDeMilanesaPollo();
        //    refrescarLv();
        //}

        private void refrescarLv()
        {
            lvCremeria.ItemsSource = null;
            lvAbarrotes.ItemsSource = null;
            lvCarniceria.ItemsSource = null;
            lvVerduras.ItemsSource = null;
            lvSalchichoneria.ItemsSource = null;
            lvTorta.ItemsSource = null;

            lvCremeria.ItemsSource = listas.MenuIngredientes[0].SubProductos;
            lvAbarrotes.ItemsSource = listas.MenuIngredientes[1].SubProductos;
            lvCarniceria.ItemsSource = listas.MenuIngredientes[2].SubProductos;
            lvVerduras.ItemsSource = listas.MenuIngredientes[3].SubProductos;
            lvSalchichoneria.ItemsSource = listas.MenuIngredientes[4].SubProductos;

            lvTorta.ItemsSource = listas.MenuTortas;
        }
        //private void tortaDeMilanesaPollo()
        //{
        //    //Digamos que esta torta de milanesa lleva:
        //    //milanesa Pollo = 100 gr
        //    //Jamon = 50 gr
        //    //quesillo = 70 gr
        //    //aguacate = 20 gr
        //    //mayonesa = 20 gr

        //    float[] tortaCantidad = new float[5] { 100, 50, 70, 20, 20 };

        //    listas.abarrotes[auxAbarrote].Cantidad -= tortaCantidad[4];
        //    listas.cremeria[auxCremeria].Cantidad -= tortaCantidad[2];
        //    listas.verduras[auxVerduras].Cantidad -= tortaCantidad[3];
        //    listas.salchichoneria[auxSalchichoneria].Cantidad -= tortaCantidad[1];
        //    listas.carniceria[auxCarniceria].Cantidad -= tortaCantidad[0];

        //    ArrayList contador = new ArrayList();

        //}

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

        //List<TortasCreador> menuTortas = new List<TortasCreador>();
        private void crearTortas()
        {
            lvAbarrotes.SelectionMode = ListViewSelectionMode.Multiple;
            lvCarniceria.SelectionMode = ListViewSelectionMode.Multiple;
            lvCremeria.SelectionMode = ListViewSelectionMode.Multiple;
            lvSalchichoneria.SelectionMode = ListViewSelectionMode.Multiple;
            lvVerduras.SelectionMode = ListViewSelectionMode.Multiple;
            bienvenidaUsuario.Text = "Creador Tortas";
            Banner.Visibility = Visibility.Visible;
            NombreTorta.Visibility = Visibility.Visible;
            CrearTorta.Visibility = Visibility.Visible;
        }

        private void CrearTortas_Click(object sender, RoutedEventArgs e)
        {
            crearTortas();
        }

        private void getDatos()
        {
            foreach (Productos item01 in lvCremeria.SelectedItems)
                torta.Add(item01);
            foreach (Productos item01 in lvAbarrotes.SelectedItems)
                torta.Add(item01);
            foreach (Productos item01 in lvCarniceria.SelectedItems)
                torta.Add(item01);
            foreach (Productos item01 in lvVerduras.SelectedItems)
                torta.Add(item01);
            foreach (Productos item01 in lvSalchichoneria.SelectedItems)
                torta.Add(item01);
        }

        private void CrearTorta_Click(object sender, RoutedEventArgs e)
        {
            getDatos();


            listas.MenuTortas.Add(new TortasCreador()
            {
                NombreTorta = NombreTorta.Text,
                PrecioTorta = 50,
                IngredientesTorta = torta
            });
            refrescarLv();
        }
    }
}
