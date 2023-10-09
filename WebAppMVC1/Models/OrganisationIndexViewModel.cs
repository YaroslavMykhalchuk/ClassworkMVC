using DemoClients;

namespace WebAppMVC1.Models
{
    public class OrganisationIndexViewModel
    {
        public DirectionSearch DirectionSearch { get; set; }
        public string FieldName { get; set; } = "Name";
        public string Filter { get; set; } = default!;
        public List<string> AvailableFields { get; set; } = new List<string>();
        public IEnumerable<Organisation>? Organisations { get; set; }
    }
}
