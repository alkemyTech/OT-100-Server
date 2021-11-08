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
    public class UserDetailsRepository : GenericRepository<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public override async Task<IEnumerable<UserDetails>> GetAll()
        {
            try
            {
                return await DbContext.UsersDetails
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(UserDetailsRepository)} GetAll method has generated an error");
                return new List<UserDetails>();
            }
        }

        public override async Task<UserDetails> GetById(int id)
        {
            try
            {
                return await DbContext.UsersDetails
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .FirstOrDefaultAsync(m => m.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(UserDetailsRepository)} GetById method has generated an error");
                return new UserDetails();
            }
        }
    }
}