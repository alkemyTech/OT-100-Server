
namespace OngProject.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { set; get; }
        public int Phone { set; get; }
        public string Email { set; get; }
        public string Message { set; get; }

    }
}
