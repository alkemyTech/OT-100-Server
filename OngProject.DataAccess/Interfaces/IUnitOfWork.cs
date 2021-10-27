using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.MemberRepository;

namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }

        Task CompleteAsync();
    }
}