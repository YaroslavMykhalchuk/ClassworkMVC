using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppMVC1.Data;
using WebAppMVC1.Models;

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
        public async Task<IActionResult> Index(string? filter, DirectionSearch directionSearch, string fieldName = "Name")
        {
            if (this._context != null)
            {
                var query = _context.Organisations.AsQueryable();

                if (!string.IsNullOrEmpty(filter))
                {
                    switch (directionSearch)
                    {
                        case DirectionSearch.startWith:
                            query = query.Where(o => EF.Property<string>(o, fieldName).StartsWith(filter));
                            break;

                        case DirectionSearch.endWith:
                            query = query.Where(o => EF.Property<string>(o, fieldName).EndsWith(filter));
                            break;

                        case DirectionSearch.contains:
                            query = query.Where(o => EF.Property<string>(o, fieldName).Contains(filter));
                            break;

                        default:
                            query = query.Where(o => EF.Property<string>(o, fieldName).StartsWith(filter));
                            break;
                    }
                }

                var model = new OrganisationIndexViewModel
                {
                    Filter = filter,
                    DirectionSearch = directionSearch,
                    FieldName = fieldName,
                    AvailableFields = typeof(Organisation).GetProperties()
                        .Where(property => property.PropertyType == typeof(string))
                        .Select(property => property.Name)
                        .ToList()
                };

                model.Organisations = await query.ToListAsync();
                return View(model);
            }
            return Problem("Entity set 'Organizations' is null");
        }

        //// GET: Organisations
        //public async Task<IActionResult> Index(string? filter, string? mode)
        //{
        //    ViewData["Filter"] = filter;
        //    ViewData["Mode"] = mode;
        //    if (_context.Organisations != null)
        //    {
        //        var query = _context.Organisations.AsQueryable();
        //        if (filter != null)
        //        {
        //            switch (mode)
        //            {
        //                case "startWith":
        //                    {
        //                        query = query.Where(s => s.Name.StartsWith(filter));
        //                    }
        //                    break;
        //                case "contains":
        //                    {
        //                        query = query.Where(s => s.Name.Contains(filter));
        //                    }
        //                    break;
        //                case "endWith":
        //                    {
        //                        query = query.Where(s => s.Name.EndsWith(filter));
        //                    }
        //                    break;
        //                default:
        //                    {
        //                        query = query.Where(s => s.Name.StartsWith(filter));
        //                    }
        //                    break;
        //            }
        //        }
        //        var organisations = await query.ToListAsync();
        //        var view = View(organisations);
        //        return view;
        //    }
        //    return Problem("Entity set 'ApplicationDbContext.Organisations'  is null.");
        //}

        // GET: Organisations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisations.FindAsync(id);
            var usersInOrganization = await _context.OrganisationUsers
                .Where(u => u.OrganisationId == id)
                .ToListAsync();

            if (organisation == null)
            {
                return NotFound();
            }

            var viewModel = new OrganisationDetailsViewModel
            {
                Organisation = organisation,
                Users = usersInOrganization
            };

            return View(viewModel);
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
