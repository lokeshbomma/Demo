using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmploiyeesController : Controller
    {
        private readonly WebApplication2Context _context;

        public EmploiyeesController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: Emploiyees
        public async Task<IActionResult> Index()
        {
              return _context.Emploiyee != null ? 
                          View(await _context.Emploiyee.ToListAsync()) :
                          Problem("Entity set 'WebApplication2Context.Emploiyee'  is null.");
        }

        // GET: Emploiyees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Emploiyee == null)
            {
                return NotFound();
            }

            var emploiyee = await _context.Emploiyee
                .FirstOrDefaultAsync(m => m.Name == id);
            if (emploiyee == null)
            {
                return NotFound();
            }

            return View(emploiyee);
        }

        // GET: Emploiyees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emploiyees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,OrderNo,Status,categoryId,ProductName,Code,Quantity")] Emploiyee emploiyee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emploiyee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emploiyee);
        }

        // GET: Emploiyees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Emploiyee == null)
            {
                return NotFound();
            }

            var emploiyee = await _context.Emploiyee.FindAsync(id);
            if (emploiyee == null)
            {
                return NotFound();
            }
            return View(emploiyee);
        }

        // POST: Emploiyees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,OrderNo,Status,categoryId,ProductName,Code,Quantity")] Emploiyee emploiyee)
        {
            if (id != emploiyee.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emploiyee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploiyeeExists(emploiyee.Name))
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
            return View(emploiyee);
        }

        // GET: Emploiyees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Emploiyee == null)
            {
                return NotFound();
            }

            var emploiyee = await _context.Emploiyee
                .FirstOrDefaultAsync(m => m.Name == id);
            if (emploiyee == null)
            {
                return NotFound();
            }

            return View(emploiyee);
        }

        // POST: Emploiyees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Emploiyee == null)
            {
                return Problem("Entity set 'WebApplication2Context.Emploiyee'  is null.");
            }
            var emploiyee = await _context.Emploiyee.FindAsync(id);
            if (emploiyee != null)
            {
                _context.Emploiyee.Remove(emploiyee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploiyeeExists(string id)
        {
          return (_context.Emploiyee?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
