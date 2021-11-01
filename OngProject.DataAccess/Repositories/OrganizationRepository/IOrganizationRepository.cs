using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;
using System.Threading.Tasks;

namespace OngProject.DataAccess.Repositories.OrganizationRepository
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Task SoftDelete(Organization organization);

    }
}
