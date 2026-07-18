using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IReservationRepository
    {
        Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Reservation?> GetAsync(CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(int id, CancellationToken cancellationToken = default);
    }
}
