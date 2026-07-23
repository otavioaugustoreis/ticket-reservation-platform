using Application.Shared;

namespace Application.UseCase.Customer
{
    public interface ISaveCustomerUseCase
    {
        Task<Result> SaveCustomerAsync();
    }
}
