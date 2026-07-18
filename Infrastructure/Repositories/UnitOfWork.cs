using Domain.Abstractions;
using Infrastructure.Abstractions;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDbSession
    {
        public IDbConnection Connection => throw new NotImplementedException();

        public IDbTransaction Transaction => throw new NotImplementedException();

        public Task BeginAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
