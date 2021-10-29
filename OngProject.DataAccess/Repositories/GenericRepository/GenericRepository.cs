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
        
        public async Task<IEnumerable<T>> GetAll()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Create(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            await Task.FromResult(DbContext.Set<T>().Update(entity));
            return entity;
        }

        public async Task Delete(T entity)
        {
            await Task.FromResult(DbContext.Set<T>().Remove(entity));
        }
    }
}