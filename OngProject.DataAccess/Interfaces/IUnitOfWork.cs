using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.OrganizationRepository;
using System.Threading.Tasks;

namespace OngProject.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        IOrganizationRepository Organizations { get; }

        Task CompleteAsync();
    }
}