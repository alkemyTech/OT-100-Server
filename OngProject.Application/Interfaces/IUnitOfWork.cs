using System.Threading.Tasks;
using OngProject.Application.Interfaces.IRepositories;

namespace OngProject.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        IActivityRepository Activities { get; }
        INewsRepository News { get; }
        ICategoryRepository Categories { get; }
        IOrganizationRepository Organizations { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        ITestimonyRepository Testimonials { get; }

        Task CompleteAsync();
    }
}