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
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public override async Task<IEnumerable<Member>> GetAll()
        {
            try
            {
                return await DbContext.Members
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(ActivityRepository)} GetAll method has generated an error");
                return new List<Member>();
            }
        }

        public override async Task<Member> GetById(int id)
        {
            try
            {
                return await DbContext.Members
                    .AsNoTracking()
                    .Where(m => m.DeletedAt == null)
                    .FirstOrDefaultAsync(m => m.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(ActivityRepository)} GetById method has generated an error");
                return new Member();
            }
        }
    }
}