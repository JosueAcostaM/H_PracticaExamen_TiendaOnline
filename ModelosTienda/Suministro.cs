using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{

    /*
    * Clase: Suministro
    * Descripción: Registra una transacción específica donde un distribuidor entrega unidades de un producto
    * a la tienda, lo cual incrementa el Stock del producto.
    * Autor: [Josué Acosta]
    * Versión: Final
    */

    public class Suministro
    {
        [Key] public int Id { get; set; }

        public int CantidadEntregada { get; set; }

        public DateTime FechaSuministro { get; set; }


        // FK (Claves Foráneas) //
        public int IdDistribuidor { get; set; }
        public int IdProducto { get; set; }


        // Objetos de Navegación //

        public Distribuidor? Distribuidor { get; set; }
        public Producto? Producto { get; set; }
    }

}

