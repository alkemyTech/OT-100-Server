namespace OngProject.Domain.Entities
{
    public abstract class BaseEntity : AuditEntityBase
    {
        public int Id { get; set; }
    }
}