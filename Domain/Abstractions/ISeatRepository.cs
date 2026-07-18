using Domain.Entities;

namespace Domain.Abstractions
{
    public interface ISeatRepository
    {
        Task<IReadOnlyList<Seat>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Seat?> GetAsync(CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(int id, CancellationToken cancellationToken = default);
    }
}
