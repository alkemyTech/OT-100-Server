using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OngProject.DataAccess.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext DbContext;
        
        public GenericRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await Task.FromResult(DbContext.Set<T>().Add(entity));
        }

        public async Task Update(T entity)
        {
            await Task.FromResult(DbContext.Set<T>().Update(entity));
        }
        
        public async Task Delete(T entity)
        {
            await Task.FromResult(DbContext.Set<T>().Remove(entity));
        }
    }
}