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
    public class NotasController : Controller
    {
        private readonly RegistroAlumnoContext _context;

        public NotasController(RegistroAlumnoContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var registroAlumnoContext = _context.Notas.Include(n => n.Asignatura).Include(n => n.Estudiante);
            return View(await registroAlumnoContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Asignatura)
                .Include(n => n.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Calificacion,Ponderacion,FechaActualizacion,AsignaturaId,EstudianteId")] Nota nota)

        {
            if (nota.Calificacion >= 1 && nota.FechaActualizacion != null && nota.EstudianteId != 0 && nota.AsignaturaId != 0)
            {
                _context.Notas.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.EstudianteId);
            return View(nota);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.EstudianteId);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Calificacion,Ponderacion,FechaActualizacion,AsignaturaId,EstudianteId")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.Id))
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
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.EstudianteId);
            return View(nota);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Asignatura)
                .Include(n => n.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notas == null)
            {
                return Problem("Entity set 'RegistroAlumnoContext.Notas'  is null.");
            }
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
          return (_context.Notas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
