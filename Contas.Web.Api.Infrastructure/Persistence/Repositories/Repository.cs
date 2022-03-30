using System.Linq.Expressions;
using Contas.Web.Api.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Contas.Web.Api.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ApplicationDbContext appDbContext;

        public DbSet<T> dbSet { get; set; }

        public Repository(ApplicationDbContext context)
        {
            appDbContext = context;
            dbSet = appDbContext.Set<T>();
        }

        public async Task<T> AddAsync(T dto)
        {
            try
            {
                await dbSet.AddAsync(dto);
                return dto;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        public void Delete(T dto)
        {
            try
            {
                dbSet.Remove(dto);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate) => await dbSet.FirstOrDefaultAsync(predicate);

        public async Task<T?> FinAsync(object Id) => await dbSet.FindAsync(Id);

        public virtual IQueryable<T>? List(Expression<Func<T, bool>>? predicate = null) =>
            ((predicate != null) ? dbSet.Where(predicate).AsNoTracking() : dbSet.AsNoTracking()) ?? null;

    }
}
