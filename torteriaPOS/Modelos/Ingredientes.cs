﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torteriaPOS
{

    public class Productos
    {
        //public string Categoria { get; set; }
        public string Producto { get; set; }
        public float Cantidad { get; set; }
        public float Precio { get; set; }
        public int Popularidad { get; set; }

        public override string ToString()
        {
            return this.Producto + "\n" + (this.Cantidad/1000) + " Kg \n$" + this.Precio;
        }
    }




    public class IngredientesCategoria
    {
        public string Categoria { get; set; }
        public List<Productos> SubProductos { get; set; }

        public override string ToString()
        {
            StringBuilder salidaCategorias = new StringBuilder();
            salidaCategorias.AppendFormat("Categoria: {0}\nProductos:\n", Categoria);
            foreach (Productos valor in SubProductos)
            {
                salidaCategorias.AppendFormat("\n{0}", valor);
            }
            return salidaCategorias.ToString();
        }
    }



    public class TortasCreador
    {
        public string NombreTorta { get; set; }
        public float PrecioTorta { get; set; }
        public List<Productos> IngredientesTorta { get; set; }

        public override string ToString()
        {

            StringBuilder salidaMenu = new StringBuilder();
            salidaMenu.AppendFormat("Torta: {0}\nPrecio: ${1}\nIngredientes:", NombreTorta, PrecioTorta);
            foreach (Productos valor in IngredientesTorta)
            {
                salidaMenu.AppendFormat("\n{0}", valor);
            }
            return salidaMenu.ToString();
        }

    }

    public class RegistroEntradas
    {
        public string FechaHora { get; set; }
        public float VentaTotal { get; set; }
        public float Ganancias { get; set; }
        public string ID { get; set; }

        //string GananciasString = string

        public override string ToString()
        {
            return this.FechaHora + "\t" + this.ID + "\t" + this.VentaTotal + "\t" + this.Ganancias +"\n";
        }
    }

    public class RegistroDiario
    {
        public string FechaDia { get; set; }
        public List<RegistroEntradas> RegistroEntradas { get; set; }

        public override string ToString()
        {

            StringBuilder salidaMenu = new StringBuilder();
            salidaMenu.AppendFormat("Fecha: {0}\n", FechaDia);
            foreach (RegistroEntradas valor in RegistroEntradas)
            {
                salidaMenu.AppendFormat("\n{0}", valor);
            }
            return salidaMenu.ToString();
        }

    }

        

}
