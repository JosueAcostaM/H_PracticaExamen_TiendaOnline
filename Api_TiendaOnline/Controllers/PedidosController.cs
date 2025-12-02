using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_TiendaOnline.Data;
using ModelosTienda;

namespace Api_TiendaOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly Api_TiendaOnlineContext _context;

        public PedidosController(Api_TiendaOnlineContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Pedido>>>> GetPedido()
        {
            try
            {
                var data = await _context.Pedidos.ToListAsync();
                return ApiResult<List<Pedido>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Pedido>>.Fail(ex.Message);
            }
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Pedido>>> GetPedido(int id)
        {
            try
            {
                var pedido = await _context
                    .Pedidos
                    .Include(e => e.Cliente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (pedido == null)
                {
                    return ApiResult<Pedido>.Fail("Datos no encontrados");
                }

                return ApiResult<Pedido>.Ok(pedido);
            }
            catch (Exception ex)
            {
                return ApiResult<Pedido>.Fail(ex.Message);
            }
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Pedido>>> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return ApiResult<Pedido>.Fail("No coinciden los identificadores");
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PedidoExists(id))
                {
                    return ApiResult<Pedido>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<Pedido>.Fail(ex.Message);
                }
            }

            return ApiResult<Pedido>.Ok(null);
        }

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Pedido>>> PostPedido(Pedido pedido)
        {
            try
            {
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                return ApiResult<Pedido>.Ok(pedido);
            }
            catch (Exception ex)
            {
                return ApiResult<Pedido>.Fail(ex.Message);
            }
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Pedido>>> DeletePedido(int id)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(id);
                if (pedido == null)
                {
                    return ApiResult<Pedido>.Fail("datos no encontrados");
                }

                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();

                return ApiResult<Pedido>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Pedido>.Fail(ex.Message);
            }
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
