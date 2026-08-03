using Application.Shared;
using Application.UseCase.Customer.Model;

namespace Application.UseCase.Customer
{
    public interface ISaveCustomerUseCase
    {
        Task<Result> ExecuteAsync(CustomerInput customer, CancellationToken cancellationToken);
    }
}
