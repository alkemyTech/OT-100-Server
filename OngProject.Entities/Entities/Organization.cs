using System.Collections.Generic;

namespace OngProject.Domain.Entities
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string WelcomeText { get; set; }
        public string AboutUsText { get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedInUrl { get; set; }

        public List<Slide> Slides { get; set; }

    }
}

   
