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

        private void refrescarLv()
        {
            lvCremeria.ItemsSource = null;
            lvAbarrotes.ItemsSource = null;
            lvCarniceria.ItemsSource = null;
            lvVerduras.ItemsSource = null;
            lvSalchichoneria.ItemsSource = null;
            lvTorta.ItemsSource = null;

            lvAbarrotes.ItemsSource = listas.MenuIngredientes[0].SubProductos;
            lvCarniceria.ItemsSource = listas.MenuIngredientes[1].SubProductos;
            lvCremeria.ItemsSource = listas.MenuIngredientes[2].SubProductos;
            lvSalchichoneria.ItemsSource = listas.MenuIngredientes[3].SubProductos;
            lvVerduras.ItemsSource = listas.MenuIngredientes[4].SubProductos;

            lvTorta.ItemsSource = agregarProdMenu;
        }

        public float auxCant;


        ArrayList pruebaCantidad = new ArrayList();
        private void lvSalchichoneria_Tapped(object sender, TappedRoutedEventArgs e)
        {
            pruebaCantidad.Add(lvSalchichoneria.SelectedIndex);
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

        public int auxCarrito;
        private void TortasMostrar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            auxCarrito = TortasMostrar.SelectedIndex;
        }



        //List<TortasCreador> menuTortas = new List<TortasCreador>();


        private void nuevoIngrediente()
        {
            //Banner1.Visibility = Visibility.Visible;
            //Banner2.Visibility = Visibility.Visible;
            //PrecioIngrediente.Visibility = Visibility.Visible;
            //CantidadIngrediente.Visibility = Visibility.Visible;
            //Banner.Visibility = Visibility.Visible;
            //NombreTorta.Visibility = Visibility.Visible;
            //NuevoIngrediente.Visibility = Visibility.Visible;
            //SeleccionCategoria.Visibility = Visibility.Visible;

            IngredientesNuevos.Visibility = Visibility.Visible;
        }

        List<Productos> PruebaProd = new List<Productos>();
        private void agregarProductos()
        {
            PruebaProd.Clear();
            PruebaProd.Add(new Productos
            {
                Producto = NombreTorta.Text,
                Cantidad = float.Parse(CantidadIngrediente.Text),
                Precio = float.Parse(PrecioIngrediente.Text)
            });
        }
        private void NuevoIngrediente_Click(object sender, RoutedEventArgs e)
        {
            agregarProductos();
            int auxIndex = SeleccionCategoria.SelectedIndex;
            listas.MenuIngredientes[auxIndex].SubProductos.AddRange(PruebaProd);
            refrescarLv();
        }
        private void GetTortas_Click(object sender, RoutedEventArgs e)
        {
            nuevoIngrediente();
        }


        private void CrearTortas_Click(object sender, RoutedEventArgs e)
        {
            NuevoIngrediente.Visibility = Visibility.Collapsed;
            IngredientesTortas.Visibility = Visibility.Visible;
            GetTortas.Visibility = Visibility.Collapsed;

            crearTortas();
        }
        private void crearTortas()
        {

            bienvenidaUsuario.Text = "Creador Tortas";
            //Banner.Visibility = Visibility.Visible;
            NombreTorta.Visibility = Visibility.Visible;
            CrearTorta.Visibility = Visibility.Visible;

            lvAbarrotes.Visibility = Visibility.Visible;
            lvCarniceria.Visibility = Visibility.Collapsed;
            lvCremeria.Visibility = Visibility.Collapsed;
            lvSalchichoneria.Visibility = Visibility.Collapsed;
            lvVerduras.Visibility = Visibility.Collapsed;

        }

        List<Productos> agregarProdMenu = new List<Productos>();
        public float precioTotal;
        public int siguiente = 0;
        public int siguienteArray = 0;

        private void agregarAMenu()
        {



            int[] arrayNext = { auxAbarrote, auxCarniceria, auxCremeria, auxSalchichoneria, auxVerduras };

            float pruebaux = float.Parse(CantidadIngredienteTorta.Text);

            float precioParcial = (pruebaux * (listas.MenuIngredientes[siguiente].SubProductos[auxAbarrote].Precio)) / 1000;

            precioTotal += precioParcial;
            agregarProdMenu.Add(new Productos()
            {

                Producto = listas.MenuIngredientes[siguiente].SubProductos[auxAbarrote].Producto,
                Cantidad = pruebaux,
                Precio = precioParcial

            }
            );


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
        private void NuevoIngredienteTorta_Click_1(object sender, RoutedEventArgs e)
        {
            lvCarniceria.Visibility = Visibility.Visible;

            agregarAMenu();
            lvCarniceria.ItemsSource = null;
            lvCarniceria.ItemsSource = agregarProdMenu;
        }

        private void CrearTorta_Click(object sender, RoutedEventArgs e)
        {
            lvCremeria.Visibility = Visibility.Visible;
            lvCremeria.ItemsSource = null;

            listas.MenuTortas.Add(new TortasCreador()
            {
                NombreTorta = NombreTortaMenu.Text,
                PrecioTorta = (precioTotal / 25) + precioTotal,
                IngredientesTorta = agregarProdMenu
            });

            //agregarProdMenu.Clear();
            precioTotal = 0;
            lvCremeria.ItemsSource = listas.MenuTortas;

        }

        private void SeleccionCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Siguiente_Click(object sender, RoutedEventArgs e)
        {

            siguiente++;
            siguienteArray++;
            lvAbarrotes.ItemsSource = null;
            lvAbarrotes.ItemsSource = listas.MenuIngredientes[siguiente].SubProductos;
            //refrescarLv();
        }



        private void AgregarCarrito_Click(object sender, RoutedEventArgs e)
        {
            SubMostrar.Visibility = Visibility.Visible;
            añadirCarrito();
            SubMostrar.ItemsSource = null;
            SubMostrar.ItemsSource = carrito;
        }
        List<TortasCreador> carrito = new List<TortasCreador>();

        private void FinalizarCarrito_Click(object sender, RoutedEventArgs e)
        {
            //TortasCreador resultado = carrito.Find();

            List<Productos> Grande = new List<Productos>();
            List<Productos> Chica = new List<Productos>();
            
            

        }

        private void añadirCarrito()
        {
            SubMostrar.ItemsSource = null;
            SubMostrar.ItemsSource = carrito;
            carrito.Add(listas.MenuTortas[auxCarrito]);
            
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Principal.Visibility = Visibility.Visible;
            TortasMostrar.Visibility = Visibility.Visible;
            TortasMostrar.ItemsSource = listas.MenuTortas;
            
        }
    }
}
