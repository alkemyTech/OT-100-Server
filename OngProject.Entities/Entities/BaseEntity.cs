namespace OngProject.Domain.Entities
{
    public abstract class BaseEntity : AuditableEntity
    {
        public int Id { get; set; }
    }
}