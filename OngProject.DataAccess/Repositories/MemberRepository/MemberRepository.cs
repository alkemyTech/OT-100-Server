using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.MemberRepository
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task SoftDelete(Member member)
        {
            await Task.FromResult(DbContext.Members.Remove(member));
        }
    }
}