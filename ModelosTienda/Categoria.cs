using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosTienda
{

    /*
    * Clase: Categoria
    * Descripción: Representa una categoría de productos en el sistema de la tienda. 
    * Contiene la información principal de la categoría y una lista de los productos asociados.
    * Autor: [Josué Acosta] 
    * Versión: Final 
    */

    public class Categoria
    {
        
        [Key] public int Id { get; set; }

        public string Nombre_Categ { get; set; }

        public string Descripcion { get; set;}


        // Objeto de Navegación //
        public List<Producto>? Productos { get; set; }= new List<Producto>();

    }
}
