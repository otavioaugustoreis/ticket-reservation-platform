namespace Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public int SeatId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.Now;
        private Reservation() { }
        public Reservation(int clientId, int seatId)
        {
            ClientId = clientId;
            SeatId = seatId;
        }
    }
}
