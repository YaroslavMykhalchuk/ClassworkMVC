using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppRazorPages1.Data;
using System.ComponentModel;
using WebAppMVC1.Models;
using System.Linq.Expressions;
using LinqKit;

namespace WebAppRazorPages1.Pages.Organisations
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Organisation> Organisation { get;set; } = default!;

        [BindProperty]
        public string Filter { get; set; }
        [BindProperty]
        public DirectionSearch DirectionSearch { get; set; }
        [BindProperty]
        public string FieldName { get; set; } = "Name";

        public async Task OnGetAsync()
        {
            if (_context.Organisations != null)
            {
                Organisation = await _context.Organisations.ToListAsync();
            }
        }
        public async Task OnPostFilterAsync()
        {
            var query = _context.Organisations.AsQueryable();

            if (!string.IsNullOrEmpty(Filter))
            {
                Search(ref query, Filter, DirectionSearch);
            }

            Organisation = await query.ToListAsync();
        }
        public void Search(ref IQueryable<Organisation> query, string filter, DirectionSearch directionSearch)
        {
            var textProperties = typeof(Organisation).GetProperties()
            .Where(p => p.PropertyType == typeof(string));

            var filterExpression = PredicateBuilder.New<Organisation>(false);

            foreach (var property in textProperties)
            {
                Expression<Func<Organisation, bool>> propertySearchExpression;

                switch (directionSearch)
                {
                    case DirectionSearch.startWith:
                        propertySearchExpression = o => EF.Property<string>(o, property.Name).StartsWith(filter);
                        break;

                    case DirectionSearch.endWith:
                        propertySearchExpression = o => EF.Property<string>(o, property.Name).EndsWith(filter);
                        break;

                    case DirectionSearch.contains:
                        propertySearchExpression = o => EF.Property<string>(o, property.Name).Contains(filter);
                        break;

                    default:
                        propertySearchExpression = o => EF.Property<string>(o, property.Name).StartsWith(filter);
                        break;
                }

                filterExpression = filterExpression.Or(propertySearchExpression);
            }

            query = query.Where(filterExpression);
        }
    }
}
