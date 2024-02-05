using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using modulAR_M.Caro.Data;
using modulAR_M.Caro.Models;

namespace modulAR_M.Caro.Controllers
{
    public class PostulacionesController : Controller
    {
        private readonly MvcModularContexto _context;

        public PostulacionesController(MvcModularContexto context)
        {
            _context = context;
        }

        // GET: Postulaciones
        public async Task<IActionResult> Index()
        {
            var mvcModularContexto = _context.Postulaciones.Include(p => p.Cliente).Include(p => p.Empleo).Include(p => p.Proceso);
            return View(await mvcModularContexto.ToListAsync());
        }

        // GET: Postulaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.Postulaciones
                .Include(p => p.Cliente)
                .Include(p => p.Empleo)
                .Include(p => p.Proceso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postulacion == null)
            {
                return NotFound();
            }

            return View(postulacion);
        }

        // GET: Postulaciones/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email");
            ViewData["EmpleoId"] = new SelectList(_context.Empleos, "Id", "Imagen");
            ViewData["ProcesoId"] = new SelectList(_context.Procesos, "Id", "Descripcion");
            return View();
        }

        // POST: Postulaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Mensaje,ClienteId,EmpleoId,ProcesoId")] Postulacion postulacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postulacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", postulacion.ClienteId);
            ViewData["EmpleoId"] = new SelectList(_context.Empleos, "Id", "Imagen", postulacion.EmpleoId);
            ViewData["ProcesoId"] = new SelectList(_context.Procesos, "Id", "Descripcion", postulacion.ProcesoId);
            return View(postulacion);
        }

        // GET: Postulaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", postulacion.ClienteId);
            ViewData["EmpleoId"] = new SelectList(_context.Empleos, "Id", "Imagen", postulacion.EmpleoId);
            ViewData["ProcesoId"] = new SelectList(_context.Procesos, "Id", "Descripcion", postulacion.ProcesoId);
            return View(postulacion);
        }

        // POST: Postulaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Mensaje,ClienteId,EmpleoId,ProcesoId")] Postulacion postulacion)
        {
            if (id != postulacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postulacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostulacionExists(postulacion.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", postulacion.ClienteId);
            ViewData["EmpleoId"] = new SelectList(_context.Empleos, "Id", "Imagen", postulacion.EmpleoId);
            ViewData["ProcesoId"] = new SelectList(_context.Procesos, "Id", "Descripcion", postulacion.ProcesoId);
            return View(postulacion);
        }

        // GET: Postulaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.Postulaciones
                .Include(p => p.Cliente)
                .Include(p => p.Empleo)
                .Include(p => p.Proceso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postulacion == null)
            {
                return NotFound();
            }

            return View(postulacion);
        }

        // POST: Postulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Postulaciones == null)
            {
                return Problem("Entity set 'MvcModularContexto.Postulaciones'  is null.");
            }
            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion != null)
            {
                _context.Postulaciones.Remove(postulacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostulacionExists(int id)
        {
          return (_context.Postulaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
