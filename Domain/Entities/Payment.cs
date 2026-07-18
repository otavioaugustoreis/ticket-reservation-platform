
namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTimeOffset? CreatedDate { get; set; } = DateTimeOffset.Now;

        private Payment() { }

        public Payment(int reservationId, PaymentStatus status)
        {
            ReservationId = reservationId;
            Status = status;
        }
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }
}
