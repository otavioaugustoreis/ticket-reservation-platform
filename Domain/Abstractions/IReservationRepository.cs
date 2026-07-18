using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IReservationRepository
    {
        Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int?> CreateAsync(Reservation reservation, CancellationToken cancellationToken = default);
    }
}
