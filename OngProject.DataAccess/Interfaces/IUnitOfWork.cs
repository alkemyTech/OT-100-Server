using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.MemberRepository;

namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        IActivityRepository Activities { get; }
        INewsRepository News { get; }


        Task CompleteAsync();
    }
}