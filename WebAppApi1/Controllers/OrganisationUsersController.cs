using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppApi1.Data;

namespace WebAppApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrganisationUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrganisationUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganisationUser>>> GetOrganisationUsers()
        {
          if (_context.OrganisationUsers == null)
          {
              return NotFound();
          }
            return await _context.OrganisationUsers.ToListAsync();
        }

        // GET: api/OrganisationUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganisationUser>> GetOrganisationUser(int id)
        {
          if (_context.OrganisationUsers == null)
          {
              return NotFound();
          }
            var organisationUser = await _context.OrganisationUsers.FindAsync(id);

            if (organisationUser == null)
            {
                return NotFound();
            }

            return organisationUser;
        }

        // PUT: api/OrganisationUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganisationUser(int id, OrganisationUser organisationUser)
        {
            if (id != organisationUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(organisationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisationUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrganisationUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrganisationUser>> PostOrganisationUser(OrganisationUser organisationUser)
        {
          if (_context.OrganisationUsers == null)
          {
              return Problem("Entity set 'AppDbContext.OrganisationUsers'  is null.");
          }
            _context.OrganisationUsers.Add(organisationUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganisationUser", new { id = organisationUser.Id }, organisationUser);
        }

        // DELETE: api/OrganisationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganisationUser(int id)
        {
            if (_context.OrganisationUsers == null)
            {
                return NotFound();
            }
            var organisationUser = await _context.OrganisationUsers.FindAsync(id);
            if (organisationUser == null)
            {
                return NotFound();
            }

            _context.OrganisationUsers.Remove(organisationUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganisationUserExists(int id)
        {
            return (_context.OrganisationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
