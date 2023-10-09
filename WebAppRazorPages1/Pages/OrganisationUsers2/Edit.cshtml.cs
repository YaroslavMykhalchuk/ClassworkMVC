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

namespace WebAppRazorPages1.Pages.OrganisationUsers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrganisationUser OrganisationUser { get; set; } = new OrganisationUser(); // Тепер створюємо новий об'єкт

        public async Task<IActionResult> OnGetAsync(int? id, int? ordId) // Додайте параметр organisationId
        {
            if (id == null)
            {
                // Створення нового користувача, передаємо OrganisationId через параметр
                OrganisationUser.OrganisationId = ordId ?? 0; // Встановлюємо значення за замовчуванням, якщо воно не вказано
                return Page();
            }

            var organisationUser = await _context.OrganisationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (organisationUser == null)
            {
                return NotFound();
            }

            OrganisationUser = organisationUser;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (OrganisationUser.Id == 0)
            {
                // Створення нового користувача, використовуємо OrganisationUser.OrganisationId
                _context.OrganisationUsers.Add(OrganisationUser);
            }
            else
            {
                // Оновлення існуючого користувача
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

            return RedirectToPage("./Index");
        }

        private bool OrganisationUserExists(int id)
        {
            return (_context.OrganisationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
