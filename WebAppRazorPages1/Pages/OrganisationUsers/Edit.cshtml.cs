using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppRazorPages1.Data;

namespace WebAppRazorPages1
{
    public class EditModel : PageModel
    {
        private readonly WebAppRazorPages1.Data.ApplicationDbContext _context;

        public EditModel(WebAppRazorPages1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrganisationUser OrganisationUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }

            var organisationuser =  await _context.OrganisationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (organisationuser == null)
            {
                return NotFound();
            }
            OrganisationUser = organisationuser;
           ViewData["OrganisationId"] = new SelectList(_context.Organisations, "Id", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrganisationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisationUserExists(OrganisationUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrganisationUserExists(int id)
        {
          return (_context.OrganisationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
