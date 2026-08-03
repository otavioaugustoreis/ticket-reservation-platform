namespace Application.UseCase.Customer.Model
{
    public record CustomerInput(string Name);

    public static class CustomerInputExtensions
    {
        public static Domain.Entities.Customer ToEntity(this CustomerInput input)
        {
            return new Domain.Entities.Customer(input.Name);
        }
    }
}
