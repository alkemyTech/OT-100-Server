using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.NewsRepository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

       public override async Task<IEnumerable<News>> GetAll()
        {
            return await DbContext.News
                .AsNoTracking()
                .Where(m => m.DeletedAt == null)
                .ToListAsync();
        }

        public override async Task<News> GetById(int id)
        {
            return await DbContext.News
                .AsNoTracking()
                .Where(m => m.DeletedAt == null)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
        }
    }
}