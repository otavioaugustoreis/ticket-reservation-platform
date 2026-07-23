using Application.UseCase.Customer;
using Microsoft.AspNetCore.Mvc;
using ticket_reservation_platform.Helpers;

namespace ticket_reservation_platform.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ISaveCustomerUseCase _saveCustomerUseCase;

        public CustomerController(ISaveCustomerUseCase saveCustomerUseCase)
        {
            _saveCustomerUseCase = saveCustomerUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCustomer()
        {
            var result = await _saveCustomerUseCase.SaveCustomerAsync();

            return result.ToActionResult();
        }
    }
}
