using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.UserRepository
{
    public interface IUserRepository: IGenericRepository<User>
    {
    }
}
