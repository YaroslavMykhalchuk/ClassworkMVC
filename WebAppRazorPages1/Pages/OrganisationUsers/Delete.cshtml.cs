using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppRazorPages1.Data;

namespace WebAppRazorPages1
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppRazorPages1.Data.ApplicationDbContext _context;

        public DeleteModel(WebAppRazorPages1.Data.ApplicationDbContext context)
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

            var organisationuser = await _context.OrganisationUsers.FirstOrDefaultAsync(m => m.Id == id);

            if (organisationuser == null)
            {
                return NotFound();
            }
            else 
            {
                OrganisationUser = organisationuser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.OrganisationUsers == null)
            {
                return NotFound();
            }
            var organisationuser = await _context.OrganisationUsers.FindAsync(id);

            if (organisationuser != null)
            {
                OrganisationUser = organisationuser;
                _context.OrganisationUsers.Remove(OrganisationUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
