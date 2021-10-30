using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.TestimonyRepository
{
    public class TestimonyRepository : GenericRepository<Testimony>, ITestimonyRepository
    {
        public TestimonyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task SoftDelete(Testimony testimony)
        {
            await Task.FromResult(DbContext.Testimonials.Remove(testimony));
        }
    }
}
