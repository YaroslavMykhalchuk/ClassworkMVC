using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClients
{
    public class OrganisationUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        [MaxLength(10)]
        public string Code { get; set; } = default!;
        public Organisation? Organisation { get; set; }
        public int OrganisationId { get; set; }

        public UserAccess AccessLevel { get; set; }

        public static implicit operator OrganisationUser(List<OrganisationUser> v)
        {
            throw new NotImplementedException();
        }
    }
}
