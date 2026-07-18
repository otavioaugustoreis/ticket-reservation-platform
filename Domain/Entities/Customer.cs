namespace Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        private Customer() { }

        public Customer(string name)
        { 
            Name = name;
        }
    }
}