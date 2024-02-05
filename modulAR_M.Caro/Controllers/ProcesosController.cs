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
    public class ProcesosController : Controller
    {
        private readonly MvcModularContexto _context;

        public ProcesosController(MvcModularContexto context)
        {
            _context = context;
        }

        // GET: Procesos
        public async Task<IActionResult> Index()
        {
              return _context.Procesos != null ? 
                          View(await _context.Procesos.ToListAsync()) :
                          Problem("Entity set 'MvcModularContexto.Procesos'  is null.");
        }

        // GET: Procesos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Procesos == null)
            {
                return NotFound();
            }

            var proceso = await _context.Procesos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proceso == null)
            {
                return NotFound();
            }

            return View(proceso);
        }

        // GET: Procesos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Procesos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] Proceso proceso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proceso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proceso);
        }

        // GET: Procesos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Procesos == null)
            {
                return NotFound();
            }

            var proceso = await _context.Procesos.FindAsync(id);
            if (proceso == null)
            {
                return NotFound();
            }
            return View(proceso);
        }

        // POST: Procesos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] Proceso proceso)
        {
            if (id != proceso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proceso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcesoExists(proceso.Id))
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
            return View(proceso);
        }

        // GET: Procesos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Procesos == null)
            {
                return NotFound();
            }

            var proceso = await _context.Procesos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proceso == null)
            {
                return NotFound();
            }

            return View(proceso);
        }

        // POST: Procesos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Procesos == null)
            {
                return Problem("Entity set 'MvcModularContexto.Procesos'  is null.");
            }
            var proceso = await _context.Procesos.FindAsync(id);
            if (proceso != null)
            {
                _context.Procesos.Remove(proceso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcesoExists(int id)
        {
          return (_context.Procesos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
