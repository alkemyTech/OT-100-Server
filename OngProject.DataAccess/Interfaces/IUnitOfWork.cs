using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.ActivityRepository;
using OngProject.DataAccess.Repositories.CategoryRepository;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.NewsRepository;
using OngProject.DataAccess.Repositories.OrganizationRepository;

namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        IActivityRepository Activities { get; }
        INewsRepository News { get; }
        ICategoryRepository Categories { get; }
        IOrganizationRepository Organizations { get; }

        Task CompleteAsync();
    }
}