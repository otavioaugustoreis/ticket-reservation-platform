
namespace Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginAsync(CancellationToken cancellationToken = default);
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollBackAsync(CancellationToken cancellationToken = default);
    }
}
