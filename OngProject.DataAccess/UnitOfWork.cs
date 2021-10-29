using System;
using System.Threading.Tasks;
using OngProject.DataAccess.Interfaces;
using OngProject.DataAccess.Repositories.MemberRepository;
using OngProject.DataAccess.Repositories.OrganizationRepository;

namespace OngProject.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Members = new MemberRepository(_dbContext);
            Organizations = new OrganizationRepository(_dbContext);
        }
        
        public IMemberRepository Members { get; }
        public IOrganizationRepository Organizations { get; }
        
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