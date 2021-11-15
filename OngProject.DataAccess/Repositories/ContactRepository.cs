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
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
        
        public override async Task<IEnumerable<Contact>> GetAll()
        {
            try
            {
                return await DbContext.Contacts
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(ContactRepository)} GetAll method has generated an error");
                return new List<Contact>();
            }
        }

        public override async Task<Contact> GetById(int id)
        {
            try
            {
                return await DbContext.Contacts
                    .AsNoTracking()
                    .Where(a => a.DeletedAt == null)
                    .FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"{typeof(ContactRepository)} GetById method has generated an error");
                return new Contact();
            }
        }
    }
}