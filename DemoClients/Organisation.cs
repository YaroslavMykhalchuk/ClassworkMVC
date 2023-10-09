using System.ComponentModel.DataAnnotations;

namespace DemoClients
{
    public class Organisation
    {
        public int Id { get; set; }
        [Display(Name = "Ім'я")]
        public string Name { get; set; } = default!; //user friendly
        public string FullName { get; set; } = default!;
        //[Range(typeof(DateTime), "01.01.2023", "30.01.2023")]
        public DateTime Created { get; set; }
        public IEnumerable<OrganisationUser>? OrganisationUsers { get; set; }

    }
}