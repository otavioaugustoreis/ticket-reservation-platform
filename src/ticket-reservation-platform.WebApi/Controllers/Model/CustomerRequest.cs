namespace ticket_reservation_platform.Controllers.Model
{
    public class CustomerRequest
    {
        public string Name { get; set; } = default!;
    }

    public static class CustomerRequestExtensions
    {
        public static Application.UseCase.Customer.Model.CustomerInput ToInput(this CustomerRequest request)
            => new Application.UseCase.Customer.Model.CustomerInput(request.Name);
        
    }
}
