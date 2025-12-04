using System.ComponentModel.DataAnnotations;

namespace ModelosTienda
{

    /*
    * Clase: Cliente
    * Descripción: Representa a un cliente individual registrado en el sistema de la tienda.
    * Contiene información básica de contacto y una lista de los pedidos realizados.
    * Autor: [Josué Acosta]
    * Versión: Final
    */

    public class Cliente
    {

        [Key] public int Id { get; set; }

        public string Nombre_Client { get; set; }

        public string Correo { get; set; }
        

        // Objeto de Navegación //
        public List<Pedido>? Pedidos { get; set; } = new List<Pedido>();


    }
}
