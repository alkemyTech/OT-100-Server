using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await DbContext.Users
                .AsNoTracking()
                .Where(m => m.DeletedAt == null)
                .ToListAsync();
        }

        public override async Task<User> GetById(int id)
        {
            return await DbContext.Users
                .AsNoTracking()
                .Where(m => m.DeletedAt == null)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
        }
    }
}