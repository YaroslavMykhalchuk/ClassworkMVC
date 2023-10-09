using DemoClients;

namespace WebAppMVC1.Models
{
    public class OrganisationDetailsViewModel
    {
        public Organisation Organisation { get; set; }
        public IEnumerable<OrganisationUser> Users { get; set; }
    }
}
