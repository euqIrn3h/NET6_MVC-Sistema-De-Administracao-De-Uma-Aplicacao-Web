using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin_MVC.Data;
using Admin_MVC.Models;

namespace Admin_MVC.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly DataContext _context;

        public CategoriaController(DataContext context)
        {
            _context = context;
        }

        // GET: CategoriaDatas
        public async Task<IActionResult> Index()
        {
              return _context.Categorias != null ? 
                          View(await _context.Categorias.ToListAsync()) :
                          Problem("Entity set 'DataContext.Categorias'  is null.");
        }

        // GET: CategoriaDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoriaData = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaData == null)
            {
                return NotFound();
            }

            return View(categoriaData);
        }

        // GET: CategoriaDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] CategoriaData categoriaData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaData);
        }

        // GET: CategoriaDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoriaData = await _context.Categorias.FindAsync(id);
            if (categoriaData == null)
            {
                return NotFound();
            }
            return View(categoriaData);
        }

        // POST: CategoriaDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] CategoriaData categoriaData)
        {
            if (id != categoriaData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaDataExists(categoriaData.Id))
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
            return View(categoriaData);
        }

        // GET: CategoriaDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoriaData = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaData == null)
            {
                return NotFound();
            }

            return View(categoriaData);
        }

        // POST: CategoriaDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'DataContext.Categorias'  is null.");
            }
            var categoriaData = await _context.Categorias.FindAsync(id);
            if (categoriaData != null)
            {
                _context.Categorias.Remove(categoriaData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaDataExists(int id)
        {
          return (_context.Categorias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
