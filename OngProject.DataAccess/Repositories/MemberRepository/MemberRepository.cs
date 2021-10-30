using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.MemberRepository
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public override async Task<IEnumerable<Member>> GetAll()
        {
            return await DbContext.Members
                .AsNoTracking()
                .Where(m => m.DeletedAt == null)
                .ToListAsync();
        }

        public override async Task<Member> GetById(int id)
        {
            return await DbContext.Members
                .AsNoTracking()
                .Where(m => m.DeletedAt == null)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
        }
    }
}