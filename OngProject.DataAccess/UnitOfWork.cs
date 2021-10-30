using System;
using System.Threading.Tasks;
using OngProject.DataAccess.Interfaces;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.NewsRepository;

namespace OngProject.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Members = new MemberRepository(_dbContext);
            News = new NewsRepository(_dbContext);
        }
        
        public IMemberRepository Members { get; }
        public INewsRepository News { get; }


        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}