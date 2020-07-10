using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;
using System;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class TokenHistoryRepository : RepositoryBase<Guid, TokenHistory>, ITokenHistoryRepository
    {
        public TokenHistoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
