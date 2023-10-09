using DemoClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebAppRazorPages1.Pages.Shared
{
    public class _OrganisationUsersModel : PageModel
    {
        //private readonly WebAppRazorPages1.Data.ApplicationDbContext _context;
        public IList<OrganisationUser> OrganisationUser { get; set; } = default!;

        public async Task OnGetAsync()
        {
            //if (_context.OrganisationUsers != null)
            //{
            //    OrganisationUser = await _context.OrganisationUsers
            //    .Include(o => o.Organisation).ToListAsync();
            //}
        }
    }
}
