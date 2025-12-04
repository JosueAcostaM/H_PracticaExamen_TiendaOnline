using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{

    /*
    * Clase: Distribuidor
    * Descripción: Representa a una compañía o persona que provee productos a la tienda.
    * Contiene información básica del distribuidor y una lista de los productos que suministra.
    * Autor: [Josué Acosta]
    * Versión: Final
    */

    public class Distribuidor
    {

        [Key] public int Id { get; set; }

        public string Nombre_Distrib { get; set; }


        // Objeto de Navegación (Colección) //

        public List<Producto>? Productos { get; set; } = new List<Producto>();
        public List<Suministro>? Suministros { get; set; } = new List<Suministro>();
    }
}
