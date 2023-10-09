using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppRazorPages1.Data;

namespace WebAppRazorPages1.Pages.Organisations
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppRazorPages1.Data.ApplicationDbContext _context;

        public DeleteModel(WebAppRazorPages1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Organisation Organisation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisations.FirstOrDefaultAsync(m => m.Id == id);

            if (organisation == null)
            {
                return NotFound();
            }
            else 
            {
                Organisation = organisation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }
            var organisation = await _context.Organisations.FindAsync(id);

            if (organisation != null)
            {
                Organisation = organisation;
                _context.Organisations.Remove(Organisation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
