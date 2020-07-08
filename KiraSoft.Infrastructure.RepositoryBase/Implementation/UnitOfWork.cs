using KiraSoft.Domain.RepositoryBase.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KiraSoft.Infrastructure.RepositoryBase.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context) { Context = context; }
        public DbContext Context { get; private set; }

        public int Commit()
        {
            try { return Context.SaveChanges(); }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                else throw e;
            }
        }

        public Task<int> CommitAsync()
        {
            try { return Context.SaveChangesAsync(); }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                else throw e;
            }
        }

        public void BeginTransaction() => Context.Database.BeginTransaction();

        public void CommitTransaction() => Context.Database.CommitTransaction();

        public void Dispose() {  if (Context != null) Context.Dispose(); }

        public void RollBackTransaction() => Context.Database.RollbackTransaction();

    }
}
