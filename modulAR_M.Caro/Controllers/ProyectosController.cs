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
    public class ProyectosController : Controller
    {
        private readonly MvcModularContexto _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProyectosController(MvcModularContexto context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            var mvcModularContexto = _context.Proyectos.Include(p => p.Categoria).Include(p => p.Cliente).Include(p => p.Ubicacion);
            return View(await mvcModularContexto.ToListAsync());
        }

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proyectos == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Categoria)
                .Include(p => p.Cliente)
                .Include(p => p.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email");
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad");
            return View();
        }

        // POST: Proyectos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public async Task<IActionResult> Create([Bind("Id,ClienteId,Titulo,Descripcion,Precio,Stock,Fecha,Escaparate,Venta,RAumentada,CategoriaId,UbicacionId")] Proyecto proyecto, IFormFile Imagen, IFormFile ImagenRA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", proyecto.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", proyecto.ClienteId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", proyecto.UbicacionId);
            return View(proyecto);
        }

        // GET: Proyectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proyectos == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", proyecto.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", proyecto.ClienteId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", proyecto.UbicacionId);
            return View(proyecto);
        }

        // POST: Proyectos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,Titulo,Descripcion,Precio,Stock,Fecha,Escaparate,Imagen,Venta,RAumentada,ImagenRA,CategoriaId,UbicacionId")] Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", proyecto.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", proyecto.ClienteId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", proyecto.UbicacionId);
            return View(proyecto);
        }

        // GET: Proyectos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proyectos == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Categoria)
                .Include(p => p.Cliente)
                .Include(p => p.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proyectos == null)
            {
                return Problem("Entity set 'MvcModularContexto.Proyectos'  is null.");
            }
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
          return (_context.Proyectos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
