using System;
using System.Collections.Generic;

namespace OngProject.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Guid IdentityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<User> Users { get; set; }
    }
}
