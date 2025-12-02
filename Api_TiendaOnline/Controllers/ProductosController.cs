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
    public class ProductosController : ControllerBase
    {
        private readonly Api_TiendaOnlineContext _context;

        public ProductosController(Api_TiendaOnlineContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Producto>>>> GetProducto()
        {
            try
            {
                var data = await _context.Productos.ToListAsync();
                return ApiResult<List<Producto>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Producto>>.Fail(ex.Message);
            }
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Producto>>> GetProducto(int id)
        {
            try
            {
                var producto = await _context
                    .Productos
                    .Include(e => e.Categoria)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (producto == null)
                {
                    return ApiResult<Producto>.Fail("Datos no encontrados");
                }

                return ApiResult<Producto>.Ok(producto);
            }
            catch (Exception ex)
            {
                return ApiResult<Producto>.Fail(ex.Message);
            }
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Producto>>> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return ApiResult<Producto>.Fail("No coinciden los identificadores");
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductoExists(id))
                {
                    return ApiResult<Producto>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<Producto>.Fail(ex.Message);
                }
            }

            return ApiResult<Producto>.Ok(null);
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Producto>>> PostProducto(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return ApiResult<Producto>.Ok(producto);
            }
            catch (Exception ex)
            {
                return ApiResult<Producto>.Fail(ex.Message);
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Producto>>> DeleteProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return ApiResult<Producto>.Fail("datos no encontrados");
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                return ApiResult<Producto>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Producto>.Fail(ex.Message);
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
