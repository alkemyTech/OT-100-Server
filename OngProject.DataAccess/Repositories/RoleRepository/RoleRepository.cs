using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.DataAccess.Repositories.RoleRepository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Role>> GetAll()
        {
            return await DbContext.Roles
                .AsNoTracking()
                .Where(m=> m.DeletedAt == null)
                .ToListAsync();
        }

        public override async Task<Role> GetById(int id)
        {
            return await DbContext.Roles
                .AsNoTracking()
                .Where(m => m.DeletedAt == null)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
        }
    }
}
