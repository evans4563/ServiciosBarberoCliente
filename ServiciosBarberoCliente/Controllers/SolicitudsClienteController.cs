using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiciosBarberoCliente.Models;

namespace ServiciosBarberoCliente.Controllers
{
    public class SolicitudsClienteController : Controller
    {
        private readonly DirectBarber1Context _context;

        public SolicitudsClienteController(DirectBarber1Context context)
        {
            _context = context;
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
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: SolicitudsCliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSolicitud,IdCliente,IdBarbero,Dirección,Fecha,Descripcion,Precio")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBarbero"] = new SelectList(_context.Usuarios, "Id", "Nombre", solicitud.IdBarbero);
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Nombre", solicitud.IdCliente);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
