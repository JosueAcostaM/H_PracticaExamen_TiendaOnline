using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{

    /*
    * Clase: Pedido
    * Descripción: Representa una orden de compra realizada por un cliente en la tienda.
    * Contiene información básica del pedido, la fecha de creación y la referencia al cliente que lo realizó.
    * Autor: [Josué Acosta]
    * Versión: Final
    */

    public class Pedido
    {
        [Key] public int Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaPedido { get; set; }


        // FK (Clave Foránea) //
        public int IdCliente { get; set; }


        // Objeto de Navegación //
        public Cliente?  Cliente { get; set; }
        public List<DetallePedido>? DetallesPedido { get; set; } = new List<DetallePedido>();
    }
}
