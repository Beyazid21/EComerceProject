using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.UnitOfWorks;
using Persistence.Context;
using Persistence.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }


        public int Save()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>()
        {
            return new ReadRepository<T>(dbContext);
        }

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>()
        {
            return new WriteRepository<T>(dbContext);
        }
    }
}
