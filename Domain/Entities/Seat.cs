namespace Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public string Number { get; set; } = default!;
        public int EventId { get; set; }

        private Seat() { }

        public Seat(string number, int eventId)
        {
            Number = number;
            EventId = eventId;
        }
    }
}
