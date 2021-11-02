using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OngProject.Application.Interfaces.IRepositories;
using OngProject.DataAccess.Context;

namespace OngProject.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly ILogger Logger;
        
        public GenericRepository(ApplicationDbContext dbContext, ILogger logger)
        {
            DbContext = dbContext;
            Logger = logger;
        }
        
        public virtual async Task<IEnumerable<T>> GetAll() => await DbContext.Set<T>().ToListAsync();

        public virtual async Task<T> GetById(int id) => await DbContext.Set<T>().FindAsync(id);

        public async Task Create(T entity) => await Task.FromResult(DbContext.Set<T>().Add(entity));

        public async Task Update(T entity) => await Task.FromResult(DbContext.Set<T>().Update(entity));

        public async Task Delete(T entity) => await Task.FromResult(DbContext.Set<T>().Remove(entity));

    }
}