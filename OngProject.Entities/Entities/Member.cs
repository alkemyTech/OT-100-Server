namespace OngProject.Domain.Entities
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}