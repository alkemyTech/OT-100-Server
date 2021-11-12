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
    public class SlideRepository : GenericRepository<Slide>, ISlideRepository
    {
        public SlideRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public override async Task<IEnumerable<Slide>> GetAll()
        {
            try
            {
                return await DbContext.Slides
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(SlideRepository)} GetAll method has generated an error");
                return new List<Slide>();
            }
        }

        public override async Task<Slide> GetById(int id)
        {
            try
            {
                return await DbContext.Slides
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .FirstOrDefaultAsync(m => m.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(SlideRepository)} GetById method has generated an error");
                return new Slide();
            }
        }
    }
}