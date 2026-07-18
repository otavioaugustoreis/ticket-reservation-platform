
using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IPaymentRepository
    {
        Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int?> CreateAsync(Payment payment, CancellationToken cancellationToken = default);
    }
}
