using ECommerce.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.Interfaces.Repositories;

namespace Persistence.Repositores
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext;

        public ReadRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get => dbContext.Set<T>(); }// hemise Set<T> yazmamaq uchun
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, bool enableTracking = false)
        {
            IQueryable<T> queryrable = Table;
            if (!enableTracking) queryrable = queryrable.AsNoTracking();//tracing dedikde update ve ya bunun kimi proseslerde gedib databasede nese deyisiklik olub olmadiqin yoxlayir  get metodlarda buna ehtiyac yoxdur elave serfiyyati qorumaq lazimdir
            if (include is not null) queryrable = include(queryrable);
            if (predicate is not null) queryrable = queryrable.Where(predicate);
            if (order is not null)
                return await order(queryrable).ToListAsync();
            return await queryrable.ToListAsync();


        }

        public async Task<IList<T>> GetAllyPagingAsyncB(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, bool enableTracking = false, int currentpage = 1, int pagesize = 3)
        {
            IQueryable<T> queryrable = Table;
            if (!enableTracking) queryrable = queryrable.AsNoTracking();//tracing dedikde update ve ya bunun kimi proseslerde gedib databasede nese deyisiklik olub olmadiqin yoxlayir  get metodlarda buna ehtiyac yoxdur elave serfiyyati qorumaq lazimdir
            if (include is not null) queryrable = include(queryrable);
            if (predicate is not null) queryrable = queryrable.Where(predicate);
            if (order is not null)
                return await order(queryrable).Skip((currentpage - 1) * pagesize).Take(pagesize).ToListAsync();
            return await queryrable.Skip((currentpage - 1) * pagesize).Take(pagesize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> queryrable = Table;
            if (!enableTracking) queryrable = queryrable.AsNoTracking();
            if (include is not null) queryrable = include(queryrable);

            return await queryrable.FirstOrDefaultAsync(predicate);

        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            Table.AsNoTracking();

            if (predicate is not null) Table.Where(predicate);
            return await Table.CountAsync();


        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            if (!enableTracking) Table.AsNoTracking();

            return Table.Where(predicate);
        }






    }
}
