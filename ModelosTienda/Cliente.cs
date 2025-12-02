using System.ComponentModel.DataAnnotations;

namespace ModelosTienda
{
    public class Cliente
    {

        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();


    }
}
