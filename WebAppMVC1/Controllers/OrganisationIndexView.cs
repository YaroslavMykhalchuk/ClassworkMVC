using DemoClients;
using WebAppMVC1.Models;

namespace WebAppMVC1.Controllers
{
    internal class OrganisationIndexView
    {
        public string Filter { get; set; }
        public DirectionSearch DirectionSearch { get; set; }
        public string FieldName { get; set; }
        public List<string> AvailableFields { get; set; }
        public List<Organisation> Organisations { get; internal set; }
    }
}