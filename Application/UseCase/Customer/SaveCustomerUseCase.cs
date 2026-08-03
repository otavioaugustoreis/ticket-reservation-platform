using Application.Shared;
using Application.UseCase.Customer.Model;
using Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Customer
{
    public class SaveCustomerUseCase : ISaveCustomerUseCase
    {
        private readonly ILogger<SaveCustomerUseCase> _logger;
        private readonly ICustomerRepository _customerRepository;

        public SaveCustomerUseCase(ILogger<SaveCustomerUseCase> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<Result> ExecuteAsync(CustomerInput input, CancellationToken cancellationToken)
        {
            try
            {
                var customer = input.ToEntity();

                await _customerRepository.CreateAsync(customer, cancellationToken);

                return Result.Success();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "[{Type}] An error occurred", nameof(SaveCustomerUseCase));

                return Result.Failed(new Error(ErrorCode.UnexpectedError, "An error occurred while saving the customer."));
            }
        }
    }
}
