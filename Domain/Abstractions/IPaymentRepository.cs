
using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IPaymentRepository
    {
        Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Payment?> GetAsync(CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(int id, CancellationToken cancellationToken = default);
    }
}
