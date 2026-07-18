using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Customer?> GetAsync(CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(int id, CancellationToken cancellationToken = default);
    }
}
