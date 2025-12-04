using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelosTienda;

namespace Api_TiendaOnline.Data
{
    public class Api_TiendaOnlineContext : DbContext
    {
        public Api_TiendaOnlineContext (DbContextOptions<Api_TiendaOnlineContext> options)
            : base(options)
        {
        }

        public DbSet<ModelosTienda.Categoria> Categorias { get; set; } = default!;
        public DbSet<ModelosTienda.Cliente> Clientes { get; set; } = default!;
        public DbSet<ModelosTienda.DetallePedido> DetallePedidos { get; set; } = default!;
        public DbSet<ModelosTienda.Pedido> Pedidos { get; set; } = default!;
        public DbSet<ModelosTienda.Producto> Productos { get; set; } = default!;
        public DbSet<ModelosTienda.Distribuidor> Distribuidor { get; set; } = default!;
        public DbSet<ModelosTienda.Suministro> Suministro { get; set; } = default!;
    }
}
