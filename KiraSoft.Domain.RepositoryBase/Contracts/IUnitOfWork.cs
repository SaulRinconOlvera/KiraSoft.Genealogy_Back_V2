using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KiraSoft.Domain.RepositoryBase.Contracts
{
    public partial interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        Task<int> CommitAsync();
        int Commit();

        void BeginTransaction();
        void RollBackTransaction();
        void CommitTransaction();
    }
}
