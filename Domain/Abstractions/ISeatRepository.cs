using Domain.Entities;

namespace Domain.Abstractions
{
    public interface ISeatRepository
    {
        Task<IReadOnlyList<Seat>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Seat?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int?> CreateAsync(Seat seat, CancellationToken cancellationToken = default);
    }
}
