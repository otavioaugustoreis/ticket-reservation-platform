using Dapper;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : SqlDataAccessBase, IPaymentRepository
    {
        private const string InsertSql = """
            INSERT INTO Payments (Id, ReservationId, Status, CreatedDate)
            VALUES (@Id, @ReservationId, @Status, @CreatedDate);

            SELECT CAST(SCOPE_IDENTITY() AS int);
        """;

        private const string SelectAllSql = """
            SELECT *
            FROM Payments
            """;

        private const string SelectByIdSql = """
            SELECT TOP (1) *
            FROM Payments
            WHERE Id = @Id
            """;

        public PaymentRepository(
            IDbSession session,
            ISqlConnectionFactory connectionFactory) : base(session, connectionFactory) {}

        public async Task<int?> CreateAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            var param = new
            {
                payment.Id,
                payment.ReservationId,
                payment.Status,
                payment.CreatedDate
            };

            var paymentId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

            if (paymentId.HasValue)
                return paymentId.Value;

            try
            {
                paymentId = await ExecuteScalarAsync<int?>(InsertSql, param, cancellationToken);
                return paymentId;
            }
            catch (SqlException ex) when (ex.Number is 2627 or 2601)
            {
                paymentId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

                if (paymentId.HasValue)
                    return paymentId.Value;

                throw;
            }
        }

        public Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return QueryAsync<Payment>(SelectAllSql, null, cancellationToken);
        }

        public Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return QueryFirstOrDefaultAsync<Payment>(SelectByIdSql, new { Id = id }, cancellationToken);
        }
    }
}
