using Dapper;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : SqlDataAccessBase, IReservationRepository
    {
        private const string InsertSql = """
            INSERT INTO Reservations (Id, ClientId, SeatId, CreatedAt, ExpiresAt)
            VALUES (@Id, @ClientId, @SeatId, @CreatedAt, @ExpiresAt);

            SELECT CAST(SCOPE_IDENTITY() AS int);
        """;

        private const string SelectAllSql = """
            SELECT *
            FROM Reservations
            """;

        private const string SelectByIdSql = """
            SELECT TOP (1) *
            FROM Reservations
            WHERE Id = @Id
            """;

        public ReservationRepository(
            IDbSession session,
            ISqlConnectionFactory connectionFactory) : base(session, connectionFactory) {}

        public async Task<int?> CreateAsync(Reservation reservation, CancellationToken cancellationToken = default)
        {
            var param = new
            {
                reservation.Id,
                reservation.ClientId,
                reservation.SeatId,
                reservation.CreatedAt,
                reservation.ExpiresAt
            };

            var reservationId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

            if (reservationId.HasValue)
                return reservationId.Value;

            try
            {
                reservationId = await ExecuteScalarAsync<int?>(InsertSql, param, cancellationToken);
                return reservationId;
            }
            catch (SqlException ex) when (ex.Number is 2627 or 2601)
            {
                reservationId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

                if (reservationId.HasValue)
                    return reservationId.Value;

                throw;
            }
        }

        public Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return QueryAsync<Reservation>(SelectAllSql, null, cancellationToken);
        }

        public Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return QueryFirstOrDefaultAsync<Reservation>(SelectByIdSql, new { Id = id }, cancellationToken);
        }
    }
}
