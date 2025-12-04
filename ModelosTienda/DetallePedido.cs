using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{

    /*
    * Clase: DetallePedido
    * Descripción: Actúa como una tabla de unión que registra la relación específica
    * entre un Pedido y un Producto, indicando qué productos forman parte de cada pedido.
    * Autor: [Josué Acosta]
    * Versión: Final
    */

    public class DetallePedido
    {

        [Key] public int Id { get; set; }

        public int CantidadComprada { get; set; }

        // FK (Claves Foráneas) //

        public int IdPedido { get; set; }
        public int IdProducto { get; set; }

        // Objetos de Navegación //

        public Pedido? Pedido { get; set; }
        public Producto? Producto { get; set; }
    }
}
