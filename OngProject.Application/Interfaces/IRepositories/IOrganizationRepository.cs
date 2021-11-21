using OngProject.Domain.Entities;
using System.Threading.Tasks;

namespace OngProject.Application.Interfaces.IRepositories
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Task<Organization> GetOrgByIdWithSlides(int id);
    }
}
