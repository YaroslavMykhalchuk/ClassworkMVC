namespace DemoClients
{
    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!; //user friendly
        public string FullName { get; set; } = default!;
        public IEnumerable<OrganisationUser>? OrganisationUsers { get; set; }

    }
}