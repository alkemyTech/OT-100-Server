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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await DbContext.Users
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(UserRepository)} GetAll method has generated an error");
                return new List<User>();
            }
        }

        public override async Task<User> GetById(int id)
        {
            try
            {
                return await DbContext.Users
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .FirstOrDefaultAsync(m => m.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(UserRepository)} GetById method has generated an error");
                return new User();
            }
        }
    }
}