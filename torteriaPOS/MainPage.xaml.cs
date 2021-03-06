﻿using System;
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




            //lvTorta.ItemsSource = torta;
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
            await Datos.escribirRegistroDiario();
        }


        private void Inventario_Click(object sender, RoutedEventArgs e)
        {

            NuevoIngrediente.Visibility = Visibility.Visible;
            IngredientesTortas.Visibility = Visibility.Collapsed;
            GetTortas.Visibility = Visibility.Visible;
            mostrarIngredientes();
            lvAbarrotes.Visibility = Visibility.Visible;
            lvCarniceria.Visibility = Visibility.Visible;
            lvCremeria.Visibility = Visibility.Visible;
            lvSalchichoneria.Visibility = Visibility.Visible;
            lvVerduras.Visibility = Visibility.Visible;
            refrescarLv();
            Principal.Visibility = Visibility.Collapsed;
            bienvenidaUsuario.Text = "Inventario";
            bienvenidaUsuario.Visibility = Visibility.Visible;
            //mostrarIngredientes();
            vistaIngredientes.Visibility = Visibility.Visible;
            ListasProductos.Visibility = Visibility.Visible;
            EstadisticasGrid.Visibility = Visibility.Collapsed;
            ResumenVentas.Visibility = Visibility.Collapsed;
            ResumenHistorico.Visibility = Visibility.Collapsed;
            PlusValorGrid.Visibility = Visibility.Collapsed;
            agregarCantidad.Visibility = Visibility.Collapsed;
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
            EstadisticasGrid.Visibility = Visibility.Collapsed;
            PlusValorGrid.Visibility = Visibility.Collapsed;

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
            EstadisticasGrid.Visibility = Visibility.Collapsed;
            ResumenVentas.Visibility = Visibility.Collapsed;
            ResumenHistorico.Visibility = Visibility.Collapsed;
            PlusValorGrid.Visibility = Visibility.Collapsed;
            agregarCantidad.Visibility = Visibility.Collapsed;

        }

        List<Productos> agregarProdMenu = new List<Productos>();
        public int siguiente = 0;
        public int siguienteArray = 0;

        private void agregarAMenu()
        {


            float pruebaux = float.Parse(CantidadIngredienteTorta.Text);

            double precioParcial = (pruebaux * (listas.MenuIngredientes[siguiente].SubProductos[auxAbarrote].Precio)) / 1000;

            //precioTotal += (float)precioParcial;
            agregarProdMenu.Add(new Productos()
            {

                Producto = listas.MenuIngredientes[siguiente].SubProductos[auxAbarrote].Producto,
                Cantidad = pruebaux,
                Precio = (float)precioParcial

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
            float precioTotal = 0;

            foreach (Productos valor in agregarProdMenu)
                precioTotal += valor.Precio;

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
            bienvenidaUsuario.Visibility = Visibility.Visible;
            for (int i = 0; i < carrito.Count; i++)
            {
                for (int a = 0; a < carrito[i].IngredientesTorta.Count; a++)
                {

                    for (int b = 0; b < listas.MenuIngredientes.Count; b++)
                    {

                        int aux = listas.MenuIngredientes[b].SubProductos.FindIndex(op => op.Producto == carrito[i].IngredientesTorta[a].Producto);
                        if (aux >= 0)
                        {
                            listas.MenuIngredientes[b].SubProductos[aux].Cantidad -= carrito[i].IngredientesTorta[a].Cantidad;
                            listas.MenuIngredientes[b].SubProductos[aux].Popularidad+=1;
                        }

                    }

                }

            }



        }
        List<TortasCreador> carrito = new List<TortasCreador>();

        private void FinalizarCarrito_Click(object sender, RoutedEventArgs e)
        {
            double preciototalCompra = 0;
            //for (int i = 0; i < carrito.Count; i++)
            //{
            //    foreach (Productos registro in carrito[i].IngredientesTorta)
            //    {
            //        preciototalCompra += registro.Precio;
            //    }
            //}


            pagar.Text = (" $" + precioTortaCarrito + " MXN");

            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            DateTime localDate = DateTime.Now;
            double preganancias = (precioTortaCarrito-(precioTortaCarrito/1.25));
            listas.Registro.Add(new RegistroEntradas()
            {
                FechaHora = localDate.ToString("yyyy/MMMM/dd"),
                Ganancias = (float)preganancias,
                VentaTotal = (float)precioTortaCarrito,
                ID = GuidString
            });

            precioTortaCarrito = 0;
            carrito.Clear();
        }
        public float precioTortaCarrito;
        private void añadirCarrito()
        {
            SubMostrar.ItemsSource = null;
            SubMostrar.ItemsSource = carrito;
            carrito.Add(listas.MenuTortas[auxCarrito]);
            precioTortaCarrito += listas.MenuTortas[auxCarrito].PrecioTorta;

        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Principal.Visibility = Visibility.Visible;
            TortasMostrar.Visibility = Visibility.Visible;
            TortasMostrar.ItemsSource = null;
            TortasMostrar.ItemsSource = listas.MenuTortas;
            IngredientesTortas.Visibility = Visibility.Collapsed;
            IngredientesNuevos.Visibility = Visibility.Collapsed;
            ListasProductos.Visibility = Visibility.Collapsed;
            EstadisticasGrid.Visibility = Visibility.Collapsed;
            ResumenVentas.Visibility = Visibility.Collapsed;
            ResumenHistorico.Visibility = Visibility.Collapsed;
            PlusValorGrid.Visibility = Visibility.Collapsed;
            agregarCantidad.Visibility = Visibility.Collapsed;
        }

        private void MostrarResumen_Click(object sender, RoutedEventArgs e)
        {
            Registro.Visibility = Visibility.Visible;
            
            Registro.ItemsSource = listas.Registro;
            Registro.Visibility = Visibility.Visible;

        }

        private List<Productos> ingredientesFavoritos()
        {
            List<Productos> listaFavoritos = new List<Productos>();

            for (int i = 0; i < listas.MenuIngredientes.Count; i++)
            {
                foreach (Productos transferir in listas.MenuIngredientes[i].SubProductos)
                    listaFavoritos.Add(transferir);
            }

            listaFavoritos.Sort((x, y) => y.Popularidad.CompareTo(x.Popularidad));

            //for (int i = 0; i <= (listaFavoritos.Count); i++)
            //{
            //    listaFavoritos.RemoveAt(i);
            //}
            List<Productos> auxFav = new List<Productos>();
            for (int i = 0; i < 5; i++)
            {
                auxFav.Add(listaFavoritos[i]);
            }
            return auxFav;

            //for (int i = 0; i < listaFavoritos.Count; i++)
            //{
            //    listaFavoritos[i].Popularidad = 0;
            //}

            //return listaFavoritos;


            //for (int i = 0; i < listas.MenuIngredientes.Count; i++)
            //{
            //    for (int j = 0; j < listas.MenuIngredientes[i].SubProductos.Count; j++)
            //    {
            //        foreach (Productos popularidad in listas.MenuIngredientes[i].SubProductos)
            //        {

            //        }
            //    }
            //}


        }

        private void Estadisiticas_Click(object sender, RoutedEventArgs e)
        {
            Principal.Visibility = Visibility.Collapsed;
            TortasMostrar.Visibility = Visibility.Collapsed;
            IngredientesTortas.Visibility = Visibility.Collapsed;
            IngredientesNuevos.Visibility = Visibility.Collapsed;
            ListasProductos.Visibility = Visibility.Collapsed;
            EstadisticasGrid.Visibility = Visibility.Visible;
            ResumenVentas.Visibility = Visibility.Collapsed;
            ResumenHistorico.Visibility = Visibility.Collapsed;
            PlusValorGrid.Visibility = Visibility.Collapsed;
            agregarCantidad.Visibility = Visibility.Collapsed;

            lvPopulares.ItemsSource = null;
            lvPopulares.ItemsSource = ingredientesFavoritos();
            lvBajaCantidad.ItemsSource = null;
            lvBajaCantidad.ItemsSource = alertaCantidad();
        }

        List <Productos> alertaCantidad()
        {
            List<Productos> cantidadPoca = new List<Productos>();
            for (int i = 0; i < listas.MenuIngredientes.Count; i++)
            {
                foreach (Productos transferir in listas.MenuIngredientes[i].SubProductos)
                {
                    if (transferir.Cantidad < 1000)
                        cantidadPoca.Add(transferir);
                    
                }

                    
                        
            }
            return cantidadPoca;
        }
        private void lvVentas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            lvVentas.ItemsSource = null;
            lvVentas.ItemsSource = listas.Registro;

        }

        private void Ventas_Click(object sender, RoutedEventArgs e)
        {

            Principal.Visibility = Visibility.Collapsed;
            TortasMostrar.Visibility = Visibility.Collapsed;
            IngredientesTortas.Visibility = Visibility.Collapsed;
            IngredientesNuevos.Visibility = Visibility.Collapsed;
            ListasProductos.Visibility = Visibility.Collapsed;
            EstadisticasGrid.Visibility = Visibility.Collapsed;
            ResumenVentas.Visibility = Visibility.Visible;
            ResumenHistorico.Visibility = Visibility.Collapsed;
            PlusValorGrid.Visibility = Visibility.Collapsed;
            agregarCantidad.Visibility = Visibility.Collapsed;
            lvVentas.ItemsSource = null;

            List<RegistroEntradas> Diario = new List<RegistroEntradas>();
            int result = 0;
            float calcBrut = 0;
            DateTime prueba = DateTime.Now;
            string prueba0 = prueba.ToString("yyyy/MM/dd");
            //for (int i = 0; i < listas.Registro.Count; i++)
            //{
            foreach (RegistroEntradas transferir in listas.Registro)
            {
                result = DateTime.Compare(DateTime.Parse(prueba0), DateTime.Parse(transferir.FechaHora));
                if (result == 0)
                    Diario.Add(transferir);
            }


            lvVentas.ItemsSource = Diario;
            tbVentaDia.Text = "Venta Bruta: "+ ventaBruta().ToString();
            tbUtilidadDia.Text = "Venta Utilidad: " + ventaUtilidad().ToString();

        }


        private float ventaUtilidad()
        {
            int result = 0;
            float calcBrut = 0;
            DateTime prueba = DateTime.Now;
            string prueba0 = prueba.ToString("yyyy/MM/dd");
            //for (int i = 0; i < listas.Registro.Count; i++)
            //{
            foreach (RegistroEntradas transferir in listas.Registro)
            {
                result = DateTime.Compare(DateTime.Parse(prueba0), DateTime.Parse(transferir.FechaHora));
                if (result == 0)
                    calcBrut += transferir.Ganancias;
            }
            return calcBrut;
            //}

        }
        private float ventaBruta()
        {
            int result = 0;
            float calcBrut = 0;
            DateTime prueba = DateTime.Now;
            string prueba0 = prueba.ToString("yyyy/MM/dd");
            //for (int i = 0; i < listas.Registro.Count; i++)
            //{
                foreach (RegistroEntradas transferir in listas.Registro)
                {
                    result = DateTime.Compare(DateTime.Parse(prueba0), DateTime.Parse(transferir.FechaHora));
                if (result == 0)
                    calcBrut += transferir.VentaTotal;
                }
            return calcBrut;
            //}
            
        }

        private void RegistroH_Click(object sender, RoutedEventArgs e)
        {
            Principal.Visibility = Visibility.Collapsed;
            TortasMostrar.Visibility = Visibility.Collapsed;
            IngredientesTortas.Visibility = Visibility.Collapsed;
            IngredientesNuevos.Visibility = Visibility.Collapsed;
            ListasProductos.Visibility = Visibility.Collapsed;
            EstadisticasGrid.Visibility = Visibility.Collapsed;
            ResumenVentas.Visibility = Visibility.Collapsed;
            ResumenHistorico.Visibility = Visibility.Visible;
            PlusValorGrid.Visibility = Visibility.Collapsed;
            agregarCantidad.Visibility = Visibility.Collapsed;
            lvVentas.ItemsSource = null;

            int result = 0;
            float calcBrut = 0;
            DateTime prueba = DateTime.Now;
            string prueba0 = prueba.ToString("yyyy/MM/dd");
            //for (int i = 0; i < listas.Registro.Count; i++)
            //{


            lvVentash.ItemsSource = null;
            lvVentash.ItemsSource = listas.Registro;
            tbVentaDiah.Text = "Venta Bruta: " + ventaBrutah().ToString();
            tbUtilidadDiah.Text = "Venta Utilidad: " + ventaUtilidadh().ToString();
        }


        private float ventaUtilidadh()
        {
            int result = 0;
            float calcBrut = 0;
 
            //for (int i = 0; i < listas.Registro.Count; i++)
            //{
            foreach (RegistroEntradas transferir in listas.Registro)
            {
                if (result == 0)
                    calcBrut += transferir.Ganancias;
            }
            return calcBrut;
            //}

        }
        private float ventaBrutah()
        {
            int result = 0;
            float calcBrut = 0;
            //for (int i = 0; i < listas.Registro.Count; i++)
            //{
            foreach (RegistroEntradas transferir in listas.Registro)
            {
                    calcBrut += transferir.VentaTotal;
            }
            return calcBrut;
            //}

        }

        private void PlusValor_Click(object sender, RoutedEventArgs e)
        {
            Principal.Visibility = Visibility.Collapsed;
            TortasMostrar.Visibility = Visibility.Collapsed;
            IngredientesTortas.Visibility = Visibility.Collapsed;
            IngredientesNuevos.Visibility = Visibility.Collapsed;
            ListasProductos.Visibility = Visibility.Collapsed;
            EstadisticasGrid.Visibility = Visibility.Collapsed;
            ResumenVentas.Visibility = Visibility.Collapsed;
            ResumenHistorico.Visibility = Visibility.Collapsed;
            PlusValorGrid.Visibility = Visibility.Visible;
            Exito.Visibility = Visibility.Collapsed;
            agregarCantidad.Visibility = Visibility.Collapsed;
            porcentaje.Text = "";
        }

        private void EnviarPlusValor_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listas.MenuIngredientes.Count; i++)
            {
                foreach (Productos cambiar in listas.MenuIngredientes[i].SubProductos)
                {
                    cambiar.Precio += (cambiar.Precio * ((float.Parse(porcentaje.Text)/100)));
                }
                Exito.Visibility = Visibility.Visible;
            }

            //for (int i = 0; i < listas.MenuTortas.Count; i++)
            //{
            //    foreach (TortasCreador cambiar in listas.MenuTortas[i].)
            //}

            foreach (TortasCreador cambiar in listas.MenuTortas)
            {
                cambiar.PrecioTorta += (cambiar.PrecioTorta * ((float.Parse(porcentaje.Text) / 100)));
            }

        }

        private void CantidadAgregar_Click(object sender, RoutedEventArgs e)
        {
            agregarCantidad.Visibility = Visibility.Visible;
            Principal.Visibility = Visibility.Collapsed;
            TortasMostrar.Visibility = Visibility.Collapsed;
            IngredientesTortas.Visibility = Visibility.Collapsed;
            IngredientesNuevos.Visibility = Visibility.Collapsed;
            ListasProductos.Visibility = Visibility.Collapsed;
            EstadisticasGrid.Visibility = Visibility.Collapsed;
            ResumenVentas.Visibility = Visibility.Collapsed;
            ResumenHistorico.Visibility = Visibility.Collapsed;
            PlusValorGrid.Visibility = Visibility.Collapsed;
            Exito.Visibility = Visibility.Collapsed;
            Exito0.Visibility = Visibility.Collapsed;
            lvModificar.ItemsSource = null;
            lvModificar.ItemsSource = listas.MenuIngredientes[0].SubProductos;


        }
        public int indexCantidad;
        public int indexAux;
        private void lvModificar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            indexCantidad = lvModificar.SelectedIndex;

        }

        private void SiguienteLista_Click(object sender, RoutedEventArgs e)
        {
            indexAux++;
            lvModificar.ItemsSource = null;
            lvModificar.ItemsSource = listas.MenuIngredientes[indexAux].SubProductos;
        }

        private void EnviarCantidad_Click(object sender, RoutedEventArgs e)
        {
            listas.MenuIngredientes[indexAux].SubProductos[indexCantidad].Cantidad += float.Parse(CantidadNueva.Text);
            Exito0.Text = "Precios de" + listas.MenuIngredientes[indexAux].SubProductos[indexCantidad].Producto + "Actualizados Correctamente";
            Exito0.Visibility = Visibility.Visible;
        }
    }
}
