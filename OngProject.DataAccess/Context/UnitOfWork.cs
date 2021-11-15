using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.IRepositories;
using OngProject.DataAccess.Repositories;

namespace OngProject.DataAccess.Context
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public UnitOfWork(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger("logs");

            Members = new MemberRepository(_dbContext, _logger);
            Activities = new ActivityRepository(_dbContext, _logger);
            News = new NewsRepository(_dbContext, _logger);
            Categories = new CategoryRepository(_dbContext, _logger);
            Organizations = new OrganizationRepository(_dbContext, _logger);
            UsersDetails = new UserDetailsRepository(_dbContext, _logger);
            Testimonials = new TestimonyRepository(_dbContext, _logger);
            Slides = new SlideRepository(_dbContext, _logger);
            Comments = new CommentRepository(_dbContext, _logger);

        }
        

        public IMemberRepository Members { get; }
        public IActivityRepository Activities { get; }
        public INewsRepository News { get; }
        public ICategoryRepository Categories { get; }
        public IOrganizationRepository Organizations { get; }
        public IUserDetailsRepository UsersDetails { get; }
        public ITestimonyRepository Testimonials { get; }
        public ISlideRepository Slides { get; }
        public ICommentRepository Comments { get; }



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