using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppRazorPages1.Data;

namespace WebAppRazorPages1.Pages.OrganisationUsers
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
