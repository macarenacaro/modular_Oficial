using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using modulAR_M.Caro.Data;
using modulAR_M.Caro.Models;

namespace modulAR_M.Caro.Controllers
{
    public class ClientesController : Controller
    {
        private readonly MvcModularContexto _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClientesController(MvcModularContexto context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var mvcModularContexto = _context.Clientes.Include(c => c.Ubicacion);
            return View(await mvcModularContexto.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nif,Nombre,Email,Telefono,Direccion,Poblacion,CodigoPostal,UbicacionId,Imagen")] Cliente cliente, IFormFile imagen)
        {
            // Verificar si se ha proporcionado una imagen
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
                cliente.Imagen = strNombreFichero;
            }


            _context.Add(cliente);
                await _context.SaveChangesAsync();
           //     return RedirectToAction(nameof(Index));
           // }
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", cliente.UbicacionId);
           // return View(cliente);

            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", cliente.UbicacionId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nif,Nombre,Email,Telefono,Direccion,Poblacion,CodigoPostal,UbicacionId")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
          //  {
                try
                {

                // Recuperar el producto original de la base de datos con la imagen actual
                var originalCliente = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

                // Restaurar la propiedad Imagen del producto original al modelo antes de actualizar
                cliente.Imagen = originalCliente.Imagen;

                _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
             //  return RedirectToAction(nameof(Index));
         //   }
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Ciudad", cliente.UbicacionId);
          //   return View(cliente);

            return RedirectToAction(nameof(Index));

        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'MvcModularContexto.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
