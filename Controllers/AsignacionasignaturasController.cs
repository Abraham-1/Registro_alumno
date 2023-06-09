using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Registro_alumno.Models;

namespace Registro_alumno.Controllers
{
    public class AsignacionasignaturasController : Controller
    {
        private readonly RegistroAlumnoContext _context;

        public AsignacionasignaturasController(RegistroAlumnoContext context)
        {
            _context = context;
        }

        // GET: Asignacionasignaturas
        public async Task<IActionResult> Index()
        {
            var registroAlumnoContext = _context.Asignacionasignaturas.Include(a => a.Asignatura).Include(a => a.Estudiante);
            return View(await registroAlumnoContext.ToListAsync());
        }

        // GET: Asignacionasignaturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asignacionasignaturas == null)
            {
                return NotFound();
            }

            var asignacionasignatura = await _context.Asignacionasignaturas
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignacionasignatura == null)
            {
                return NotFound();
            }

            return View(asignacionasignatura);
        }

        // GET: Asignacionasignaturas/Create
        public IActionResult Create()
        {
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: Asignacionasignaturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AsignaturaId,EstudianteId,FechaRegistro")] Asignacionasignatura asignacionasignatura)
        {
            if (asignacionasignatura.EstudianteId!= 0 && asignacionasignatura.AsignaturaId != 0)
            {
                _context.Asignacionasignaturas.Add(asignacionasignatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignacionasignatura.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignacionasignatura.EstudianteId);
            return View(asignacionasignatura);
        }

        // GET: Asignacionasignaturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asignacionasignaturas == null)
            {
                return NotFound();
            }

            var asignacionasignatura = await _context.Asignacionasignaturas.FindAsync(id);
            if (asignacionasignatura == null)
            {
                return NotFound();
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignacionasignatura.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignacionasignatura.EstudianteId);
            return View(asignacionasignatura);
        }

        // POST: Asignacionasignaturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AsignaturaId,EstudianteId,FechaRegistro")] Asignacionasignatura asignacionasignatura)
        {
            if (id != asignacionasignatura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignacionasignatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionasignaturaExists(asignacionasignatura.Id))
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
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignacionasignatura.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignacionasignatura.EstudianteId);
            return View(asignacionasignatura);
        }

        // GET: Asignacionasignaturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asignacionasignaturas == null)
            {
                return NotFound();
            }

            var asignacionasignatura = await _context.Asignacionasignaturas
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignacionasignatura == null)
            {
                return NotFound();
            }

            return View(asignacionasignatura);
        }

        // POST: Asignacionasignaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asignacionasignaturas == null)
            {
                return Problem("Entity set 'RegistroAlumnoContext.Asignacionasignaturas'  is null.");
            }
            var asignacionasignatura = await _context.Asignacionasignaturas.FindAsync(id);
            if (asignacionasignatura != null)
            {
                _context.Asignacionasignaturas.Remove(asignacionasignatura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignacionasignaturaExists(int id)
        {
          return (_context.Asignacionasignaturas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
