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
        IUserDetailsRepository UsersDetails { get; }
        ITestimonyRepository Testimonials { get; }
        ISlideRepository Slides { get; }
        IContactRepository Contacts { get; }
        ICommentRepository Comments { get; }

        Task CompleteAsync();
    }
}