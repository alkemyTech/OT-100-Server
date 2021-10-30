using System;
using System.Threading.Tasks;
using OngProject.DataAccess.Interfaces;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.RoleRepository;
using OngProject.DataAccess.Repositories.TestimonyRepository;

namespace OngProject.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Members = new MemberRepository(_dbContext);
            Roles = new RoleRepository(_dbContext);
            Testimonials = new TestimonyRepository(_dbContext);
        }
        
        public IMemberRepository Members { get; }
        public IRoleRepository Roles { get; }
        public ITestimonyRepository Testimonials { get; }
        
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