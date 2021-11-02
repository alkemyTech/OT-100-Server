
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
    public class TestimonyRepository : GenericRepository<Testimony>, ITestimonyRepository
    {
        public TestimonyRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
        
        public override async Task<IEnumerable<Testimony>> GetAll()
        {
            try
            {
                return await DbContext.Testimonials
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(TestimonyRepository)} GetAll method has generated an error");
                return new List<Testimony>();
            }
        }

        public override async Task<Testimony> GetById(int id)
        {
            try
            {
                return await DbContext.Testimonials
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(TestimonyRepository)} GetById method has generated an error");
                return new Testimony();
            }
        }
    }
}