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
    public class ClientesController : ControllerBase
    {
        private readonly Api_TiendaOnlineContext _context;

        public ClientesController(Api_TiendaOnlineContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Cliente>>>> GetCliente()
        {
            try
            {
                var data = await _context.Clientes.ToListAsync();
                return ApiResult<List<Cliente>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Cliente>>.Fail(ex.Message);
            }
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Cliente>>> GetCliente(int id)
        {
            try
            {
                var cliente = await _context
                    .Clientes
                    .Include(e => e.Pedidos)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (cliente == null)
                {
                    return ApiResult<Cliente>.Fail("Datos no encontrados");
                }

                return ApiResult<Cliente>.Ok(cliente);
            }
            catch (Exception ex)
            {
                return ApiResult<Cliente>.Fail(ex.Message);
            }
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Cliente>>> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return ApiResult<Cliente>.Fail("No coinciden los identificadores");
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ClienteExists(id))
                {
                    return ApiResult<Cliente>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<Cliente>.Fail(ex.Message);
                }
            }

            return ApiResult<Cliente>.Ok(null);

        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Cliente>>> PostCliente(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return ApiResult<Cliente>.Ok(cliente);
            }
            catch (Exception ex)
            {
                return ApiResult<Cliente>.Fail(ex.Message);
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Cliente>>> DeleteCliente(int id)
        {
            try
            {
                var cliente = await _context.Categorias.FindAsync(id);
                if (cliente == null)
                {
                    return ApiResult<Cliente>.Fail("datos no encontrados");
                }

                _context.Categorias.Remove(cliente);
                await _context.SaveChangesAsync();

                return ApiResult<Cliente>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Cliente>.Fail(ex.Message);
            }
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
