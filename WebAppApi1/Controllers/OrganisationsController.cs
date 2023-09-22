using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppApi1.Data;

namespace WebAppDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationsController : ControllerBase
    {

        private readonly AppDbContext _context;

        public OrganisationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Organisations
        [HttpGet]
        public async Task<ActionResult<QueryResult<Organisation>>> GetOrganisations(int skip = 0, int take = 50)
        {
            if (_context.Organisations == null)
            {
                return NotFound();
            }
            QueryResult<Organisation> result = new QueryResult<Organisation> { Items = await _context.Organisations.Skip(skip).Take(take).ToListAsync(), Count = await _context.Organisations.CountAsync() };
            
            return Ok(new QueryResult<Organisation> { Items = await _context.Organisations.Skip(skip).Take(take).ToListAsync(), Count = await _context.Organisations.CountAsync() });
        }

        // GET: api/Organisations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organisation>> GetOrganisation(int id)
        {
            if (_context.Organisations == null)
            {
                return NotFound();
            }
            var organization = await _context.Organisations.FindAsync(id);

            if (organization == null)
            {
                return NotFound();
            }

            return organization;
        }

        // PUT: api/Organisations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganisation(int id, Organisation organization)
        {
            if (id != organization.Id)
            {
                return BadRequest();
            }

            _context.Entry(organization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisationExists(id))
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

        // POST: api/Organisations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organisation>> PostOrganisation(Organisation organization)
        {
            if (_context.Organisations == null)
            {
                return Problem("Entity set 'AppDbContext.Organizations'  is null.");
            }
            _context.Organisations.Add(organization);
            await _context.SaveChangesAsync();
            var result = CreatedAtAction("GetOrganisation", new { id = organization.Id }, organization);
            return result;
        }

        // DELETE: api/Organisations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganisation(int id)
        {
            if (_context.Organisations == null)
            {
                return NotFound();
            }
            var organization = await _context.Organisations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            _context.Organisations.Remove(organization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganisationExists(int id)
        {
            return (_context.Organisations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
