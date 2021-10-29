using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.CategoryRepository;
using OngProject.DataAccess.Repositories.MemberRepository;

namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        ICategoryRepository Categories { get; }

        Task CompleteAsync();
    }
}