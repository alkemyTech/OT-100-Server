using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OngProject.Domain.Entities;
using OngProject.Application.Interfaces.IRepositories;
using OngProject.DataAccess.Context;

namespace OngProject.DataAccess.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
        
        public override async Task<IEnumerable<Organization>> GetAll()
        {
            try
            {
                return await DbContext.Organizations
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(OrganizationRepository)} GetAll method has generated an error");
                return new List<Organization>();
            }
        }

        public override async Task<Organization> GetById(int id)
        {
            try
            {
                return await DbContext.Organizations
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(OrganizationRepository)} GetById method has generated an error");
                return new Organization();
            }
        }

        public async Task<Organization> GetOrgByIdWithSlides(int id)
        {
            try
            {
                return await DbContext.Organizations
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .Include(a => a.Slides.OrderBy(s => s.Order))
                    .FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(OrganizationRepository)} GetById method has generated an error");
                return new Organization();
            }
        }
    }
}
