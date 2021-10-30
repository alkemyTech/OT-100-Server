using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.UserRepository;

namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }

        IUserRepository Users { get; }

        Task CompleteAsync();
    }
}