using ECommerce.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : class, IEntityBase, new()
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,

            Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, bool enableTracking = false);

        //burada Func<IQueryable<T>,IIncludableQueryable<T,object>>? include=null->tableleler arasinda kechid uchun include ve theninclude istifade ede bilmek uchun
        // Func<IQueryable<T>,IOrderedQueryable<T>>? order=null-->bu ise tablenin herhansi propertisine gore siralamaq uchun

        Task<IList<T>> GetAllyPagingAsyncB(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,

            Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, bool enableTracking = false, int currentpage = 1, int pagesize = 3);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false);//burada herhansi entitini caqirarken mutleq bir sert olmalidi ona gore prediczte null ola bilmez

        IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);


    }
}
