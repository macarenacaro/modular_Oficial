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
    public class EmpleosController : Controller
    {
        private readonly MvcModularContexto _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmpleosController(MvcModularContexto context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Empleos
        public async Task<IActionResult> Index()
        {
            var mvcModularContexto = _context.Empleos.Include(e => e.Categoria).Include(e => e.Cliente).Include(e => e.Ubicacion);
            return View(await mvcModularContexto.ToListAsync());
        }

        // GET: Empleos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleos == null)
            {
                return NotFound();
            }

            var empleo = await _context.Empleos
                .Include(e => e.Categoria)
                .Include(e => e.Cliente)
                .Include(e => e.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleo == null)
            {
                return NotFound();
            }

            return View(empleo);
        }

        // GET: Empleos/Create
        public IActionResult Create()
        {
            var empleo = new Empleo
            {
                Fecha = DateTime.Today // o DateTime.Now, según tus necesidades
            };
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email");
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad");
            return View(empleo);
        }

        // POST: Empleos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,Titulo,Descripcion,Fecha,Salario,Imagen,Escaparate,CategoriaId,UbicacionId")] Empleo empleo, IFormFile imagen)
        {
            //if (ModelState.IsValid)
           // {

                // Verificar si se ha proporcionado una imagen         
                if (imagen != null && imagen.Length > 0)
                {
                    // Copiar archivo de imagen
                    string strRutaImagenes = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes");
                    string strExtension = Path.GetExtension(imagen.FileName);
                    string strNombreFichero = Guid.NewGuid().ToString() + strExtension;
                    string strRutaFichero = Path.Combine(strRutaImagenes, strNombreFichero);
                    using (var fileStream = new FileStream(strRutaFichero, FileMode.Create))
                    {
                        imagen.CopyTo(fileStream);
                    }

                    // Asignar el nombre del archivo al producto
                    empleo.Imagen = strNombreFichero;
                }
                _context.Add(empleo);
                await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
           // }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", empleo.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", empleo.ClienteId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", empleo.UbicacionId);
            //  return View(empleo);
            return RedirectToAction(nameof(Index));
        }

        // GET: Empleos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleos == null)
            {
                return NotFound();
            }

            var empleo = await _context.Empleos.FindAsync(id);
            empleo.Fecha = DateTime.Today;

            if (empleo == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", empleo.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", empleo.ClienteId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", empleo.UbicacionId);
            return View(empleo);
        }

        // POST: Empleos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,Titulo,Descripcion,Fecha,Salario,Imagen,Escaparate,CategoriaId,UbicacionId")] Empleo empleo)
        {
            if (id != empleo.Id)
            {
                return NotFound();
            }

       //     if (ModelState.IsValid)
         //   {
                try
                {

                // Recuperar el producto original de la base de datos con la imagen actual
                var originalEmpleo = await _context.Empleos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

                // Restaurar la propiedad Imagen del producto original al modelo antes de actualizar
                empleo.Imagen = originalEmpleo.Imagen;

                _context.Update(empleo);
                await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleoExists(empleo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
             //   return RedirectToAction(nameof(Index));
        //  }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", empleo.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Email", empleo.ClienteId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", empleo.UbicacionId);
            //   return View(empleo);
            return RedirectToAction(nameof(Index));
        }

        // GET: Empleos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleos == null)
            {
                return NotFound();
            }

            var empleo = await _context.Empleos
                .Include(e => e.Categoria)
                .Include(e => e.Cliente)
                .Include(e => e.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleo == null)
            {
                return NotFound();
            }

            return View(empleo);
        }

        // POST: Empleos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleos == null)
            {
                return Problem("Entity set 'MvcModularContexto.Empleos'  is null.");
            }
            var empleo = await _context.Empleos.FindAsync(id);
            if (empleo != null)
            {
                _context.Empleos.Remove(empleo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleoExists(int id)
        {
          return (_context.Empleos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
