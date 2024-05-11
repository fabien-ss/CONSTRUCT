using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Entities;

namespace AspnetCoreMvcFull
{
    public class ChauffeurController : Controller
    {
        private readonly Prom13 _context;

        public ChauffeurController(Prom13 context)
        {
            _context = context;
        }

        // GET: Chauffeur
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chauffeur.ToListAsync());
        }

        // GET: Chauffeur/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chauffeur = await _context.Chauffeur
                .FirstOrDefaultAsync(m => m.IdChauffeur == id);
            if (chauffeur == null)
            {
                return NotFound();
            }

            return View(chauffeur);
        }

        // GET: Chauffeur/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chauffeur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChauffeur,NomPrenom,DateNaissance,Numero,Email,MotDePasse,Sexe")] Chauffeur chauffeur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chauffeur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chauffeur);
        }

        // GET: Chauffeur/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chauffeur = await _context.Chauffeur.FindAsync(id);
            if (chauffeur == null)
            {
                return NotFound();
            }
            return View(chauffeur);
        }

        // POST: Chauffeur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdChauffeur,NomPrenom,DateNaissance,Numero,Email,MotDePasse,Sexe")] Chauffeur chauffeur)
        {
            if (id != chauffeur.IdChauffeur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chauffeur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChauffeurExists(chauffeur.IdChauffeur))
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
            return View(chauffeur);
        }

        // GET: Chauffeur/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chauffeur = await _context.Chauffeur
                .FirstOrDefaultAsync(m => m.IdChauffeur == id);
            if (chauffeur == null)
            {
                return NotFound();
            }

            return View(chauffeur);
        }

        // POST: Chauffeur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chauffeur = await _context.Chauffeur.FindAsync(id);
            if (chauffeur != null)
            {
                _context.Chauffeur.Remove(chauffeur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChauffeurExists(string id)
        {
            return _context.Chauffeur.Any(e => e.IdChauffeur == id);
        }
    }
}
