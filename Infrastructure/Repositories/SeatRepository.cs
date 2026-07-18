using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class SeatRepository : SqlDataAccessBase, ISeatRepository
    {
        private const string InsertSql = """
            INSERT INTO Seats (Id, Number, EventId)
            VALUES (@Id, @Number, @EventId);

            SELECT CAST(SCOPE_IDENTITY() AS int);
        """;

        private const string SelectAllSql = """
            SELECT *
            FROM Seats
            """;

        private const string SelectByIdSql = """
            SELECT TOP (1) *
            FROM Seats
            WHERE Id = @Id
            """;

        public SeatRepository(
            IDbSession session,
            ISqlConnectionFactory connectionFactory) : base(session, connectionFactory) {}

        public async Task<int?> CreateAsync(Seat seat, CancellationToken cancellationToken = default)
        {
            var param = new
            {
                seat.Id,
                seat.Number,
                seat.EventId
            };

            var seatId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

            if (seatId.HasValue)
                return seatId.Value;

            try
            {
                seatId = await ExecuteScalarAsync<int?>(InsertSql, param, cancellationToken);
                return seatId;
            }
            catch (SqlException ex) when (ex.Number is 2627 or 2601)
            {
                seatId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

                if (seatId.HasValue)
                    return seatId.Value;

                throw;
            }
        }

        public Task<IReadOnlyList<Seat>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return QueryAsync<Seat>(SelectAllSql, null, cancellationToken);
        }

        public Task<Seat?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return QueryFirstOrDefaultAsync<Seat>(SelectByIdSql, new { Id = id }, cancellationToken);
        }
    }
}
