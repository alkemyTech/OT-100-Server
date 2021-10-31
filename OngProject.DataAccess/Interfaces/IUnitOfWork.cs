using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.ActivityRepository;
using OngProject.DataAccess.Repositories.CategoryRepository;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.NewsRepository;

namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        IActivityRepository Activities { get; }
        INewsRepository News { get; }
        ICategoryRepository Categories { get; }

        Task CompleteAsync();
    }
}