using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.DataAccess.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}