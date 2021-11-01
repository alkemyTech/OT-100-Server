using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;
using System.Threading.Tasks;

namespace OngProject.DataAccess.Repositories.OrganizationRepository
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task SoftDelete(Organization organization)
        {
            await Task.FromResult(DbContext.Organizations.Remove(organization));
        }
    }
}
