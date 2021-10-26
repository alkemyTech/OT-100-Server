using System;

namespace OngProject.Domain.Entities
{
    public class AuditEntityBase
    {
        private DateTime? _createdAt;
        public DateTime CreatedAt
        {
            get => _createdAt ??= DateTime.Now;
        }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}