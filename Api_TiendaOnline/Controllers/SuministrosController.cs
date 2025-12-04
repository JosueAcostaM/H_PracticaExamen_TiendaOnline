using Api_TiendaOnline.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelosTienda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_TiendaOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuministrosController : ControllerBase
    {
        private readonly Api_TiendaOnlineContext _context;

        public SuministrosController(Api_TiendaOnlineContext context)
        {
            _context = context;
        }

        // GET: api/Suministros
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Suministro>>>> GetSuministro()
        {
            try
            {
                var data = await _context.Suministro
                    .Include(s => s.Distribuidor)
                    .Include(s => s.Producto)
                    .ToListAsync();

                return ApiResult<List<Suministro>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Suministro>>.Fail(ex.Message);
            }
        }

        // GET: api/Suministros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Suministro>>> GetSuministro(int id)
        {
            try
            {
                var suministro = await _context.Suministro
                    .Include(s => s.Distribuidor)
                    .Include(s => s.Producto)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (suministro == null)
                {
                    return ApiResult<Suministro>.Fail("Suministro no encontrado");
                }

                return ApiResult<Suministro>.Ok(suministro);
            }
            catch (Exception ex)
            {
                return ApiResult<Suministro>.Fail(ex.Message);
            }
        }

        // POST: api/Suministros
        [HttpPost]
        public async Task<ActionResult<ApiResult<Suministro>>> PostSuministro(Suministro suministro)
        {
            if (suministro.CantidadEntregada <= 0)
            {
                return ApiResult<Suministro>.Fail("La cantidad entregada debe ser mayor a cero.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Suministro.Add(suministro);
                await _context.SaveChangesAsync();

                var producto = await _context.Productos.FindAsync(suministro.IdProducto);

                if (producto == null)
                {
                    transaction.Rollback();
                    return ApiResult<Suministro>.Fail($"Producto con ID {suministro.IdProducto} no encontrado.");
                }

                producto.Stock += suministro.CantidadEntregada;

                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return ApiResult<Suministro>.Ok(suministro);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ApiResult<Suministro>.Fail($"Error al registrar el suministro y actualizar el stock: {ex.Message}");
            }
        }


        // PUT: api/Suministros/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Suministro>>> PutSuministro(int id, Suministro suministro)
        {
   

            if (id != suministro.Id)
            {
                return ApiResult<Suministro>.Fail("No coinciden los identificadores");
            }

            _context.Entry(suministro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return ApiResult<Suministro>.Ok(null);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuministroExists(id))
                {
                    return ApiResult<Suministro>.Fail("Datos no encontrados");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return ApiResult<Suministro>.Fail(ex.Message);
            }
        }

        // DELETE: api/Suministros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Suministro>>> DeleteSuministro(int id)
        {

            try
            {
                var suministro = await _context.Suministro.FindAsync(id);
                if (suministro == null)
                {
                    return ApiResult<Suministro>.Fail("datos no encontrados");
                }

                _context.Suministro.Remove(suministro);
                await _context.SaveChangesAsync();

                return ApiResult<Suministro>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Suministro>.Fail(ex.Message);
            }
        }


        private bool SuministroExists(int id)
        {
            return _context.Suministro.Any(e => e.Id == id);
        }
    }
}