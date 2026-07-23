using Application.Shared;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Customer
{
    public class SaveCustomerUseCase : ISaveCustomerUseCase
    {
        private readonly ILogger<SaveCustomerUseCase> _logger

        public SaveCustomerUseCase(ILogger<SaveCustomerUseCase> logger)
        {
            _logger = logger;
        }

        public async Task<Result> SaveCustomerAsync()
        {
            try
            {

            }catch(Exception ex)
            {

            }
        }
    }
}
