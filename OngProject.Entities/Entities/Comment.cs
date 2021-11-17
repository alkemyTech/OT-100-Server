namespace OngProject.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public int UserId { get; set; }
        public string Body { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
