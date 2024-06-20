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
;    public class FinitionController : Controller
    {
        private readonly ConstructionDb _context;
        private Utilisateur _utilisateur;

        public FinitionController(ConstructionDb context)
        {
            _context = context;/*
            string user = HttpContext.Session.GetString("user");
            _utilisateur = JsonSerializer.Deserialize<Utilisateur>(user);*/
        }

        // GET: Finition
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
            return View(await _context.Finitions.ToListAsync());
        }

        // GET: Finition/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var finition = await _context.Finitions
                .FirstOrDefaultAsync(m => m.IdFinition == id);
            if (finition == null)
            {
                return NotFound();
            }

            return View(finition);
        }

        // GET: Finition/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Finition/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFinition,Finition1,Majoration,Photo")] Finition finition)
        {

            if (ModelState.IsValid)
            {
                _context.Add(finition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(finition);
        }

        // GET: Finition/Edit/5
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

            var finition = await _context.Finitions.FindAsync(id);
            if (finition == null)
            {
                return NotFound();
            }
            return View(finition);
        }

        // POST: Finition/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdFinition,Finition1,Majoration,Photo")] Finition finition)
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
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finition);
                     _context.SaveChanges();
                      return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                  ModelState.AddModelError("error ", e.Message);
                }
            }
            return View(finition);
        }

        // GET: Finition/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finition = await _context.Finitions
                .FirstOrDefaultAsync(m => m.IdFinition == id);
            if (finition == null)
            {
                return NotFound();
            }

            return View(finition);
        }

        // POST: Finition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var finition = await _context.Finitions.FindAsync(id);
            if (finition != null)
            {
                _context.Finitions.Remove(finition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinitionExists(int id)
        {
            return _context.Finitions.Any(e => e.IdFinition == id);
        }
    }
