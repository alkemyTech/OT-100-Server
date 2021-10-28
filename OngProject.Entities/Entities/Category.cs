using System.Collections.Generic;

namespace OngProject.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public ICollection<News> News { get; set; }
    }
}
