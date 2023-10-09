using DemoClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppRazorPages1.Data;


namespace WebAppRazorPages1.Pages.Organisations
{
    public class EditUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrganisationUser OrganisationUser { get; set; } = new OrganisationUser();

        public async Task<IActionResult> OnGetAsync(int? id, int? organisationId)
        {
            if (id == null)
            {
                if(organisationId != null)
                    OrganisationUser.OrganisationId = (int)organisationId;
                return Page();
            }

            var organisationUser = await _context.OrganisationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (organisationUser == null)
            {
                return NotFound();
            }
            else
            {
                OrganisationUser = organisationUser;
            }

            //OrganisationUser = organisationUser;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (OrganisationUser.Id == 0) // ÿךשמ id == 0 עמ צו ף םאס םמגטי ‏חונ
            {
                _context.OrganisationUsers.Add(OrganisationUser);
            }
            else
            {
                _context.Attach(OrganisationUser).State = EntityState.Modified;
            }

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

            return RedirectToPage($"./Details", new { id = OrganisationUser.OrganisationId });
        }

        private bool OrganisationUserExists(int id)
        {
            return (_context.OrganisationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
