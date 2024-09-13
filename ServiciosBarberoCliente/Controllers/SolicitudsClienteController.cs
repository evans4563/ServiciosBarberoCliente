using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiciosBarberoCliente.Models;
using System.Security.Claims;

namespace ServiciosBarberoCliente.Controllers
{
    public class SolicitudsClienteController : Controller
    {
        private readonly DirectBarber1Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SolicitudsClienteController(DirectBarber1Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: SolicitudsCliente
        public async Task<IActionResult> Index()
        {
            var directBarber1Context = _context.Solicituds.Include(s => s.IdBarberoNavigation).Include(s => s.IdClienteNavigation);
            return View(await directBarber1Context.ToListAsync());
        }

        // GET: SolicitudsCliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var solicitud = await _context.Solicituds
                .Include(s => s.IdBarberoNavigation)
                .Include(s => s.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: SolicitudsCliente/Create
        public IActionResult Create()
        {
            ViewData["IdBarbero"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: SolicitudsCliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSolicitud,IdBarbero,Dirección,Fecha,Descripcion,Precio")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                // Obtener el ID del usuario autenticado
                var userIdString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdString, out var userId))
                {
                    solicitud.IdCliente = userId;
                }
                else
                {
                    // Manejar el caso en que el ID del usuario no se puede obtener
                    ModelState.AddModelError(string.Empty, "No se pudo obtener el ID del usuario.");
                    return View(solicitud);
                }

                _context.Add(solicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["IdBarbero"] = new SelectList(_context.Usuarios, "Id", "Nombre", solicitud.IdBarbero);
            return View(solicitud);
        }

        // GET: SolicitudsCliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicituds.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            ViewData["IdBarbero"] = new SelectList(_context.Usuarios, "Id", "Nombre", solicitud.IdBarbero);
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Nombre", solicitud.IdCliente);
            return View(solicitud);
        }

        // POST: SolicitudsCliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSolicitud,IdCliente,IdBarbero,Dirección,Fecha,Descripcion,Precio")] Solicitud solicitud)
        {
            if (id != solicitud.IdSolicitud)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudExists(solicitud.IdSolicitud))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBarbero"] = new SelectList(_context.Usuarios, "Id", "Nombre", solicitud.IdBarbero);
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Nombre", solicitud.IdCliente);
            return View(solicitud);
        }

        // GET: SolicitudsCliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicituds
                .Include(s => s.IdBarberoNavigation)
                .Include(s => s.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // POST: SolicitudsCliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitud = await _context.Solicituds.FindAsync(id);
            if (solicitud != null)
            {
                _context.Solicituds.Remove(solicitud);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudExists(int id)
        {
            return _context.Solicituds.Any(e => e.IdSolicitud == id);
        }
    }
}
