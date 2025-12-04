using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{
    public class Distribuidor
    {
        [Key] public int Id { get; set; }

        public string Nombre_Distrib { get; set; }
        public int Cantidad_Produc {  get; set; }
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
