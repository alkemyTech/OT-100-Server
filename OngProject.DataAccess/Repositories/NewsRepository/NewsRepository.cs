using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.NewsRepository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task SoftDelete(News news)
        {
            await Task.FromResult(DbContext.News.Remove(news));
        }
    }
}