using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZennoLabTestProject.Domain.Interfaces;

namespace ZennoLabTestProject.Domain
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly ZennoLabDbContext _dbContext;
        public EfRepository(ZennoLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Save(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> Get()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}