using System.Collections.Generic;

namespace OngProject.Domain.Entities
{
    public class News : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
