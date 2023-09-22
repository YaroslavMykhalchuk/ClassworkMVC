﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppMVC1.Data;

namespace WebAppMVC1.Controllers
{
    public class OrganisationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganisationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organisations
        public async Task<IActionResult> Index()
        {
            return _context.Organisations != null ?
                        View(await _context.Organisations.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Organisations'  is null.");
        }

        // GET: Organisations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organisation == null)
            {
                return NotFound();
            }

            return View(organisation);
        }

        // GET: Organisations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organisations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FullName")] Organisation organisation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organisation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organisation);
        }

        // GET: Organisations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisations.FindAsync(id);
            if (organisation == null)
            {
                return NotFound();
            }
            return View(organisation);
        }

        // POST: Organisations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FullName")] Organisation organisation)
        {
            if (id != organisation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organisation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganisationExists(organisation.Id))
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
            return View(organisation);
        }

        // GET: Organisations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organisation == null)
            {
                return NotFound();
            }

            return View(organisation);
        }

        // POST: Organisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Organisations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Organisations'  is null.");
            }
            var organisation = await _context.Organisations.FindAsync(id);
            if (organisation != null)
            {
                _context.Organisations.Remove(organisation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganisationExists(int id)
        {
            return (_context.Organisations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
