using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{
    public class Producto
    {
        [Key] public int Id { get; set; }
        public string Nombre_Prod { get; set; }
        
        public int Stock { get; set; }

        public float PrecioUnitario { get; set; }

        public int IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
