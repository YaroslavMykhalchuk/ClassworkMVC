using System.Collections.Generic;

namespace DemoClients
{
    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; } //user friendly
        public string FullName { get; set; }
        public IEnumerable<OrganisationUser> OrganisationUsers { get; set; }

    }
}