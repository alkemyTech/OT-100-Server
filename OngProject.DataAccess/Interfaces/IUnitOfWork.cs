using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.NewsRepository;


namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        INewsRepository News { get; }


        Task CompleteAsync();
    }
}