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
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public override async Task<IEnumerable<Activity>> GetAll()
        {
            try
            {
                return await DbContext.Activities
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(ActivityRepository)} GetAll method has generated an error");
                return new List<Activity>();
            }
        }

        public override async Task<Activity> GetById(int id)
        {
            try
            {
                return await DbContext.Activities
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(ActivityRepository)} GetById method has generated an error");
                return new Activity();
            }
        }
    }
}