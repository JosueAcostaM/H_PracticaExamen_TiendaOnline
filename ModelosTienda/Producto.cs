using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{

    /*
    * Clase: Producto
    * Descripción: Representa un artículo específico disponible para la venta en la tienda.
    * Contiene información sobre el stock, precio, nombre y la categoría a la que pertenece.
    * Autor: [Josué Acosta]
    * Versión: Final
    */

    public class Producto
    {
        [Key] public int Id { get; set; }

        public string Nombre_Prod { get; set; }

        public int Stock { get; set; }

        public float PrecioUnitario { get; set; }


        // FK (Clave Foránea) //
        public int IdCategoria { get; set; }

        // Objeto de Navegación //
        public Categoria? Categoria { get; set; }
        public List<Suministro>? Suministros { get; set; } = new List<Suministro>();
    }
}
