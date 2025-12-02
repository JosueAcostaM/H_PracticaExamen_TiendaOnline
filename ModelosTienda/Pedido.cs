using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{
    public class Pedido
    {
        [Key] public int Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaPedido { get; set; }

        public int IdCliente { get; set; }
        public Cliente?  Cliente { get; set; }
    }
}
