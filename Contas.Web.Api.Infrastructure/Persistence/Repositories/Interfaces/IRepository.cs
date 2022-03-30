using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Contas.Web.Api.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> dbSet { get; set; }
        IQueryable<T>? List(Expression<Func<T, bool>>? predicate = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FinAsync(object Id);
        Task<T> AddAsync(T dto);
        void Delete(T dto);
    }
}
