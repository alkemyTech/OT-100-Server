using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.ActivityRepository
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Activity>> GetAll()
        {
            return await DbContext.Activities
                .AsNoTracking()
                .Where(a => a.DeletedAt == null)
                .ToListAsync();
        }

        public override async Task<Activity> GetById(int id)
        {
            return await DbContext.Activities
                .AsNoTracking()
                .Where(a => a.DeletedAt == null)
                .FirstOrDefaultAsync(a => a.Id.Equals(id));
        }
    }
}