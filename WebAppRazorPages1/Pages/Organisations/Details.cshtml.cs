using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppRazorPages1.Data;
using WebAppRazorPages1.Pages.Shared;

namespace WebAppRazorPages1.Pages.Organisations
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppRazorPages1.Data.ApplicationDbContext _context;

        public DetailsModel(WebAppRazorPages1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Organisation? Organisation { get; set; } = default!;
        public IList<OrganisationUser>? OrganisationUsers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Organisations == null)
            {
                return NotFound();
            }

            Organisation = await _context.Organisations.FirstOrDefaultAsync(m => m.Id == id);
            OrganisationUsers = await _context.OrganisationUsers
                .Where(u => u.OrganisationId == id)
                .ToListAsync();
            if (Organisation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
