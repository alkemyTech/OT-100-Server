using System;
using System.Threading.Tasks;
using OngProject.Domain.Entities;

namespace OngProject.Application.Interfaces.IRepositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetByGuid(Guid guid);
    }
}