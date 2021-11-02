using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OngProject.Application.Interfaces.IRepositories;
using OngProject.DataAccess.Context;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

       public override async Task<IEnumerable<News>> GetAll()
        {
            try
            {
                return await DbContext.News
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(ActivityRepository)} GetAll method has generated an error");
                return new List<News>();
            }
        }

        public override async Task<News> GetById(int id)
        {
            try
            {
                return await DbContext.News
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .FirstOrDefaultAsync(m => m.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(ActivityRepository)} GetById method has generated an error");
                return new News();
            }
        }
    }
}