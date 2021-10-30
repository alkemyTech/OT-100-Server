using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.RoleRepository;
using OngProject.DataAccess.Repositories.TestimonyRepository;

namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        IRoleRepository Roles { get; }
        ITestimonyRepository Testimonials { get; }

        Task CompleteAsync();
    }
}