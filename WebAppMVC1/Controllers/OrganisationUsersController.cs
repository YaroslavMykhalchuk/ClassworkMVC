using System;
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
    public class OrganisationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganisationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrganisationUsers
        public async Task<IActionResult> Index(int? orgId)
        {
            var applicationDbContext = await _context.OrganisationUsers
                .Where(u => u.OrganisationId == orgId)
                .ToListAsync();
            //var applicationDbContext = _context.OrganisationUsers.Include(o => o.Organisation);
            return View(applicationDbContext);
        }

        // GET: OrganisationUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }

            var organisationUser = await _context.OrganisationUsers
                .Include(o => o.Organisation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organisationUser == null)
            {
                return NotFound();
            }
            return View(organisationUser);
        }

        // GET: OrganisationUsers/Create
        public IActionResult Create()
        {
            ViewData["OrganisationId"] = new SelectList(_context.Organisations, "Id", "FullName");
            return View();
        }

        // POST: OrganisationUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OrganisationId,AccessLevel")] OrganisationUser organisationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organisationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganisationId"] = new SelectList(_context.Organisations, "Id", "FullName", organisationUser.OrganisationId);
            return View(organisationUser);
        }

        // GET: OrganisationUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }

            var organisationUser = await _context.OrganisationUsers.FindAsync(id);
            if (organisationUser == null)
            {
                return NotFound();
            }
            ViewData["OrganisationId"] = new SelectList(_context.Organisations, "Id", "FullName", organisationUser.OrganisationId);
            return View(organisationUser);
        }

        // POST: OrganisationUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OrganisationId,AccessLevel")] OrganisationUser organisationUser)
        {
            if (id != organisationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organisationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganisationUserExists(organisationUser.Id))
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
            ViewData["OrganisationId"] = new SelectList(_context.Organisations, "Id", "FullName", organisationUser.OrganisationId);
            return View(organisationUser);
        }

        // GET: OrganisationUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }

            var organisationUser = await _context.OrganisationUsers
                .Include(o => o.Organisation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organisationUser == null)
            {
                return NotFound();
            }

            return View(organisationUser);
        }

        // POST: OrganisationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrganisationUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrganisationUsers'  is null.");
            }
            var organisationUser = await _context.OrganisationUsers.FindAsync(id);
            if (organisationUser != null)
            {
                _context.OrganisationUsers.Remove(organisationUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganisationUserExists(int id)
        {
          return (_context.OrganisationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
