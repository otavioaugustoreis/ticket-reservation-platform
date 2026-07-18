using Domain.Entities;

namespace Domain.Abstractions
{
    public interface ICustomerRepository
    {
        Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(Customer customer, CancellationToken cancellationToken = default);
    }
}
