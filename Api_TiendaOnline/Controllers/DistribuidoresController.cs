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
    public class DistribuidoresController : ControllerBase
    {
        private readonly Api_TiendaOnlineContext _context;

        public DistribuidoresController(Api_TiendaOnlineContext context)
        {
            _context = context;
        }

        // GET: api/Distribuidores
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Distribuidor>>>> GetDistribuidor()
        {
            try
            {
                var data = await _context.Distribuidor.ToListAsync();
                return ApiResult<List<Distribuidor>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Distribuidor>>.Fail(ex.Message);
            }
        }

        // GET: api/Distribuidores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Distribuidor>>> GetDistribuidor(int id)
        {
            try
            {
                var distribuidor = await _context
                    .Distribuidor
                    .Include(e => e.Productos)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (distribuidor == null)
                {
                    return ApiResult<Distribuidor>.Fail("Datos no encontrados");
                }

                return ApiResult<Distribuidor>.Ok(distribuidor);
            }
            catch (Exception ex)
            {
                return ApiResult<Distribuidor>.Fail(ex.Message);
            }
        }

        // PUT: api/Distribuidores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Distribuidor>>> PutDistribuidor(int id, Distribuidor distribuidor)
        {
            if (id != distribuidor.Id)
            {
                return ApiResult<Distribuidor>.Fail("No coinciden los identificadores");
            }

            _context.Entry(distribuidor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DistribuidorExists(id))
                {
                    return ApiResult<Distribuidor>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<Distribuidor>.Fail(ex.Message);
                }
            }

            return ApiResult<Distribuidor>.Ok(null);
        }

        // POST: api/Distribuidores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Distribuidor>>> PostDistribuidor(Distribuidor distribuidor)
        {
            try
            {
                _context.Distribuidor.Add(distribuidor);
                await _context.SaveChangesAsync();

                return ApiResult<Distribuidor>.Ok(distribuidor);
            }
            catch (Exception ex)
            {
                return ApiResult<Distribuidor>.Fail(ex.Message);
            }
        }

        // DELETE: api/Distribuidores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Distribuidor>>> DeleteDistribuidor(int id)
        {
            try
            {
                var distribuidor = await _context.Categorias.FindAsync(id);
                if (distribuidor == null)
                {
                    return ApiResult<Distribuidor>.Fail("datos no encontrados");
                }

                _context.Categorias.Remove(distribuidor);
                await _context.SaveChangesAsync();

                return ApiResult<Distribuidor>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Distribuidor>.Fail(ex.Message);
            }
        }

        private bool DistribuidorExists(int id)
        {
            return _context.Distribuidor.Any(e => e.Id == id);
        }
    }
}
