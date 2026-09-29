using Application.Shared;
using Application.UseCase.Customer;
using Microsoft.AspNetCore.Mvc;
using ticket_reservation_platform.Controllers.Model;
using ticket_reservation_platform.Helpers;

namespace ticket_reservation_platform.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ISaveCustomerUseCase _saveCustomerUseCase;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ISaveCustomerUseCase saveCustomerUseCase)
        {
            _saveCustomerUseCase = saveCustomerUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCustomer([FromBody] CustomerRequest input, CancellationToken cancellationToken)
        {
            try
            {


                var result = await _saveCustomerUseCase.ExecuteAsync(input.ToInput(), cancellationToken);

                return result.ToActionResult();
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "[{Type}] Operation was canceled.", nameof(CustomerController));

                return Result.Failed(new (ErrorCode.RequestTimeout, "An error occurred while saving the customer.")).ToActionResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Type}] An error occurred", nameof(CustomerController));

                return Result.Failed(new (ErrorCode.UnexpectedError, "An error occurred while saving the customer.")).ToActionResult();
            }
        }
    }
}
