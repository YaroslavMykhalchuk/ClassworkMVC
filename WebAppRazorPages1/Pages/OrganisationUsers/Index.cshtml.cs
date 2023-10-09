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
    public class IndexModel : PageModel
    {
        private readonly WebAppRazorPages1.Data.ApplicationDbContext _context;

        public IndexModel(WebAppRazorPages1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrganisationUser> OrganisationUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OrganisationUsers != null)
            {
                OrganisationUser = await _context.OrganisationUsers
                .Include(o => o.Organisation).ToListAsync();
            }
        }
    }
}
