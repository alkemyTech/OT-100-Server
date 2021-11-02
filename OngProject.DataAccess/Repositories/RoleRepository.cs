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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
        
        public override async Task<IEnumerable<Role>> GetAll()
        {
            try
            {
                return await DbContext.Roles
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(RoleRepository)} GetAll method has generated an error");
                return new List<Role>();
            }
        }

        public override async Task<Role> GetById(int id)
        {
            try
            {
                return await DbContext.Roles
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(RoleRepository)} GetById method has generated an error");
                return new Role();
            }
        }
    }
}