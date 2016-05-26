using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torteriaPOS
{
    public class Ingredientes 
    {
        public string Producto { get; set; }
        public float Cantidad { get; set; }
        public float Precio { get; set; }
        public string Categoria { get; set; }


    }

    public class MostrarIngredientes : Ingredientes
    {
        public override string ToString()
        {
            return this.Producto + "\n" + (this.Cantidad/1000) + " Kg \n$" + this.Precio;
        }
    }
}
