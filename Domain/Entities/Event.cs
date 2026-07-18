namespace Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

        private Event() { }

        public Event(string name, DateTimeOffset date)
        {
            Name = name;
            Date = date;
        }
    }
}
