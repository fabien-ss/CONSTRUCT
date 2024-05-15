using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.Entities;

namespace AspnetCoreMvcFull.Controllers
{
    public class PrestationController : Controller
    {
        private readonly ConstructionDb _context;

        public PrestationController(ConstructionDb context)
        {
            _context = context;
        }

        // GET: Prestation
        public async Task<IActionResult> Index()
        {
          try
          {
            string user = HttpContext.Session.GetString("user");
            Utilisateur userq = JsonSerializer.Deserialize<Utilisateur>(user);
            if (userq.Privilege < 10) throw new Exception("User non autorisé");
          }
          catch (Exception e)
          {
            return RedirectToAction("LoginBasic","Auth");
          }
            var constructionDb = _context.Prestations.Include(p => p.IdPrestation1Navigation).Include(p => p.IdTypeTravauxNavigation).Include(p => p.IdUniteNavigation);
            return View(await constructionDb.ToListAsync());
        }

        // GET: Prestation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestation = await _context.Prestations
                .Include(p => p.IdPrestation1Navigation)
                .Include(p => p.IdTypeTravauxNavigation)
                .Include(p => p.IdUniteNavigation)
                .FirstOrDefaultAsync(m => m.IdPrestation == id);
            if (prestation == null)
            {
                return NotFound();
            }

            return View(prestation);
        }

        // GET: Prestation/Create
        public IActionResult Create()
        {
            ViewData["IdPrestation1"] = new SelectList(_context.Prestations, "IdPrestation", "IdPrestation");
            ViewData["IdTypeTravaux"] = new SelectList(_context.TypeTravauxes, "IdTypeTravaux", "IdTypeTravaux");
            ViewData["IdUnite"] = new SelectList(_context.Unites, "IdUnite", "IdUnite");
            return View();
        }

        // POST: Prestation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrestation,Prestation1,Code,PrixUnitaire,Duree,IdUnite,IdPrestation1,IdTypeTravaux")] Prestation prestation)
        {

            if (ModelState.IsValid)
            {
                _context.Add(prestation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPrestation1"] = new SelectList(_context.Prestations, "IdPrestation", "IdPrestation", prestation.IdPrestation1);
            ViewData["IdTypeTravaux"] = new SelectList(_context.TypeTravauxes, "IdTypeTravaux", "IdTypeTravaux", prestation.IdTypeTravaux);
            ViewData["IdUnite"] = new SelectList(_context.Unites, "IdUnite", "IdUnite", prestation.IdUnite);
            return View(prestation);
        }

        // GET: Prestation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
          try
          {
            string user = HttpContext.Session.GetString("user");
            Utilisateur userq = JsonSerializer.Deserialize<Utilisateur>(user);
            if (userq.Privilege < 10) throw new Exception("User non autorisé");
          }
          catch (Exception e)
          {
            return RedirectToAction("LoginBasic","Auth");
          }
            if (id == null)
            {
                return NotFound();
            }

            var prestation = await _context.Prestations.FindAsync(id);
            if (prestation == null)
            {
                return NotFound();
            }
            ViewData["IdPrestation1"] = new SelectList(_context.Prestations, "IdPrestation", "IdPrestation", prestation.IdPrestation1);
            ViewData["IdTypeTravaux"] = new SelectList(_context.TypeTravauxes, "IdTypeTravaux", "IdTypeTravaux", prestation.IdTypeTravaux);
            ViewData["IdUnite"] = new SelectList(_context.Unites, "IdUnite", "IdUnite", prestation.IdUnite);
            return View(prestation);
        }

        // POST: Prestation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("IdPrestation,Prestation1,Code,PrixUnitaire,Duree,IdUnite,IdPrestation1,IdTypeTravaux")] Prestation prestation)
        { try
          {
            string user = HttpContext.Session.GetString("user");
            Utilisateur userq = JsonSerializer.Deserialize<Utilisateur>(user);
            if (userq.Privilege < 10) throw new Exception("User non autorisé");
          }
          catch (Exception e)
          {
            return RedirectToAction("LoginBasic","Auth");
          }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestationExists(prestation.IdPrestation))
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
            ViewData["IdPrestation1"] = new SelectList(_context.Prestations, "IdPrestation", "IdPrestation", prestation.IdPrestation1);
            ViewData["IdTypeTravaux"] = new SelectList(_context.TypeTravauxes, "IdTypeTravaux", "IdTypeTravaux", prestation.IdTypeTravaux);
            ViewData["IdUnite"] = new SelectList(_context.Unites, "IdUnite", "IdUnite", prestation.IdUnite);
            return View(prestation);
        }

        // GET: Prestation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestation = await _context.Prestations
                .Include(p => p.IdPrestation1Navigation)
                .Include(p => p.IdTypeTravauxNavigation)
                .Include(p => p.IdUniteNavigation)
                .FirstOrDefaultAsync(m => m.IdPrestation == id);
            if (prestation == null)
            {
                return NotFound();
            }

            return View(prestation);
        }

        // POST: Prestation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestation = await _context.Prestations.FindAsync(id);
            if (prestation != null)
            {
                _context.Prestations.Remove(prestation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestationExists(int id)
        {
            return _context.Prestations.Any(e => e.IdPrestation == id);
        }
    }
}
