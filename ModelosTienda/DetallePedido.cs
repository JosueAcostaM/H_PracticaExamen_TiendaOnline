using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{
    public class DetallePedido
    {
        [Key] public int Id { get; set; }
        
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }

        public Pedido? Pedido { get; set; }

        public Producto? Producto { get; set; }
    }
}
