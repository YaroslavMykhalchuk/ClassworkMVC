using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoClients;
using WebAppRazorPages1.Data;

namespace WebAppRazorPages1
{
    public class CreateModel : PageModel
    {
        private readonly WebAppRazorPages1.Data.ApplicationDbContext _context;

        public CreateModel(WebAppRazorPages1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrganisationId"] = new SelectList(_context.Organisations, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public OrganisationUser OrganisationUser { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.OrganisationUsers == null || OrganisationUser == null)
            {
                return Page();
            }

            _context.OrganisationUsers.Add(OrganisationUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
