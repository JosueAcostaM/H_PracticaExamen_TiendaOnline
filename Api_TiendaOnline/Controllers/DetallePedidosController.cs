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
    public class DetallePedidosController : ControllerBase
    {
        private readonly Api_TiendaOnlineContext _context;

        public DetallePedidosController(Api_TiendaOnlineContext context)
        {
            _context = context;
        }

        // GET: api/DetallePedidos
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<DetallePedido>>>> GetDetallePedido()
        {
            try
            {
                var data = await _context.DetallePedidos.ToListAsync();
                return ApiResult<List<DetallePedido>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<DetallePedido>>.Fail(ex.Message);
            }
        }

        // GET: api/DetallePedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<DetallePedido>>> GetDetallePedido(int id)
        {
            try
            {
                var detalle = await _context
                    .DetallePedidos
                    .Include(e => e.Producto)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (detalle == null)
                {
                    return ApiResult<DetallePedido>.Fail("Datos no encontrados");
                }

                return ApiResult<DetallePedido>.Ok(detalle);
            }
            catch (Exception ex)
            {
                return ApiResult<DetallePedido>.Fail(ex.Message);
            }
        }

        // PUT: api/DetallePedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<DetallePedido>>> PutDetallePedido(int id, DetallePedido detallePedido)
        {
            if (id != detallePedido.Id)
            {
                return ApiResult<DetallePedido>.Fail("No coinciden los identificadores");
            }

            _context.Entry(detallePedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DetallePedidoExists(id))
                {
                    return ApiResult<DetallePedido>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<DetallePedido>.Fail(ex.Message);
                }
            }

            return ApiResult<DetallePedido>.Ok(null);
        }

        // POST: api/DetallePedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<DetallePedido>>> PostDetallePedido(DetallePedido detallePedido)
        {
            try
            {
                _context.DetallePedidos.Add(detallePedido);
                await _context.SaveChangesAsync();

                return ApiResult<DetallePedido>.Ok(detallePedido);
            }
            catch (Exception ex)
            {
                return ApiResult<DetallePedido>.Fail(ex.Message);
            }
        }

        // DELETE: api/DetallePedidos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<DetallePedido>>> DeleteDetallePedido(int id)
        {
            try
            {
                var detalle = await _context.DetallePedidos.FindAsync(id);
                if (detalle == null)
                {
                    return ApiResult<DetallePedido>.Fail("datos no encontrados");
                }

                _context.DetallePedidos.Remove(detalle);
                await _context.SaveChangesAsync();

                return ApiResult<DetallePedido>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<DetallePedido>.Fail(ex.Message);
            }
        }

        private bool DetallePedidoExists(int id)
        {
            return _context.DetallePedidos.Any(e => e.Id == id);
        }
    }
}
