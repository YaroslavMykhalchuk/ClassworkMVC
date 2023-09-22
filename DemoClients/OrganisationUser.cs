using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClients
{
    public class OrganisationUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public Organisation? Organisation { get; set; }
        public int OrganisationId { get; set; }

        public UserAccess AccessLevel { get; set; }
    }
}
