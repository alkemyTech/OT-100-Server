using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.MemberRepository
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task SoftDelete(Member member);
    }
}