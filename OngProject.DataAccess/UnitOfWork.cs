using System;
using System.Threading.Tasks;
using OngProject.DataAccess.Interfaces;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.UserRepository;

namespace OngProject.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Members = new MemberRepository(_dbContext);
            Users = new UserRepository(_dbContext);
        }
        
        public IMemberRepository Members { get; }
        public IUserRepository Users { get; }

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