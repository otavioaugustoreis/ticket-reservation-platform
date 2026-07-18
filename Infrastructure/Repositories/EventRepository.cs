using Dapper;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class EventRepository : SqlDataAccessBase, IEventRepository
    {

        private const string InsertSql = """
            INSERT INTO Events (Id, Name)
            VALUES (@Id, @Name);

            SELECT CAST(SCOPE_IDENTITY() AS int);
        """;

        private const string SelectAllSql = """
            SELECT * 
            FROM Events
            """;

        private const string SelectByIdSql = """
            SELECT TOP (1) Id
            FROM Events WHERE Id = @Id
            """;

        public EventRepository(
            IDbSession session,
            ISqlConnectionFactory connectionFactory) : base(session, connectionFactory) {}

        public async Task<int?> CreateAsync(Event events, CancellationToken cancellationToken = default)
        {
            var param = new
            {
                events.Id,
                events.Name
            };

            var eventsId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

            if (eventsId.HasValue)
                return eventsId.Value;

            try
            {
                eventsId = await ExecuteScalarAsync<int?>(InsertSql, param, cancellationToken);
                return eventsId!.Value;
            }
            catch (SqlException ex) when (ex.Number is 2627 or 2601)
            {
                eventsId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

                if (eventsId.HasValue)
                    throw;

                return eventsId.Value;
            }
        }

        public async Task<IReadOnlyList<Event>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await QueryAsync<Event>(SelectAllSql, null, cancellationToken);
            return products.AsList();
        }

        //Arrumar selects
        public async Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await QueryFirstOrDefaultAsync<Event>(SelectByIdSql, new { Id = id }, cancellationToken);
    }
}
