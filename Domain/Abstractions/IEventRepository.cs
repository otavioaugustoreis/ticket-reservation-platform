using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IEventRepository
    {
        Task<IReadOnlyList<Event>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int?> CreateAsync(Event events, CancellationToken cancellationToken = default);
    }
}
