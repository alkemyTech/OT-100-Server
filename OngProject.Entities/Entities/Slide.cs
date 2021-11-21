namespace OngProject.Domain.Entities
{
    public class Slide : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

    }
}