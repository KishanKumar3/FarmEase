using FarmEase.Domain.Helper;
using FarmEase.Infrastructure.Data;
using FarmEase.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FarmEase.Infrastructure.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            dbSet = _dbContext.Set<T>(); // dynamically gets the correct table
        }
        public async Task<T> Add(T entity)
        {
            var addedEntity = await dbSet.AddAsync(entity);
            return addedEntity.Entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.AnyAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query = tracked ? dbSet : dbSet.AsNoTracking();

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(Constants.Separator.Comma, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query = tracked ? dbSet.Where(filter) : dbSet.AsNoTracking().Where(filter);

            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in includeProperties.Split(Constants.Separator.Comma, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
