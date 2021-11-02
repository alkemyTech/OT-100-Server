using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OngProject.Domain.Entities;
using OngProject.Application.Interfaces.IRepositories;
using OngProject.DataAccess.Context;

namespace OngProject.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
        
        public override async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                return await DbContext.Categories
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(CategoryRepository)} GetAll method has generated an error");
                return new List<Category>();
            }
        }

        public override async Task<Category> GetById(int id)
        {
            try
            {
                return await DbContext.Categories
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(CategoryRepository)} GetById method has generated an error");
                return new Category();
            }
        }
    }
}
