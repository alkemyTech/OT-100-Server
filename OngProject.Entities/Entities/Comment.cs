namespace OngProject.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public int UserDetailsId { get; set; }
        public UserDetails UserDetails { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public string Body { get; set; }
    }
}
