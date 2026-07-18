using Dapper;
using Infrastructure.Abstractions;

namespace Infrastructure
{
    public abstract class SqlDataAccessBase
    {
        private readonly IDbSession _session;
        private readonly ISqlConnectionFactory _connectionFactory;

        protected SqlDataAccessBase(IDbSession session, ISqlConnectionFactory connectionFactory)
        {
            _session = session;
            _connectionFactory = connectionFactory;
        }

        protected virtual async Task<int> ExecuteAsync(
            string sql, 
            object? param = null,
            CancellationToken cancellationToken = default)
        {
            if(_session.Transaction is not null)
                return await _session.Transaction.Connection!.ExecuteAsync(
                    new CommandDefinition(sql, param, cancellationToken: cancellationToken));
            
            using var conn = _connectionFactory.CreateConnection();
            return await conn.ExecuteAsync(
                new CommandDefinition(sql, param, cancellationToken: cancellationToken));
        }

        protected virtual async Task<T?> ExecuteScalarAsync<T>(
            string sql,
            object? param = null,
            CancellationToken cancellationToken = default)
        {
            if (_session.Transaction is not null)
                return await _session.Transaction.Connection!.ExecuteScalarAsync<T>(
                    new CommandDefinition(sql, param, cancellationToken: cancellationToken));

            using var conn = _connectionFactory.CreateConnection();
            return await conn.ExecuteScalarAsync<T>(
                new CommandDefinition(sql, param, cancellationToken: cancellationToken));
        }

        protected virtual async Task<IReadOnlyList<T>> QueryAsync<T>(
            string sql,
            object? param = null,
            CancellationToken cancellationToken = default)
        {
            if (_session.Transaction is not null)
            {
                var rows = await _session.Transaction.Connection!.QueryAsync<T>(
                    new CommandDefinition(sql, param, cancellationToken: cancellationToken));
                return rows.AsList();
            }
                
            using var conn = _connectionFactory.CreateConnection();
            var result =  await conn.QueryAsync<T>(
                new CommandDefinition(sql, param, cancellationToken: cancellationToken));

            return result.AsList();
        }

        protected virtual async Task<T?> QuerySingleOrDefaultAsync<T>(
            string sql,
            object? param = null,
            CancellationToken cancellationToken = default)
        {
            if (_session.Transaction is not null)
                return await _session.Transaction.Connection!.QuerySingleOrDefaultAsync<T>(
                new CommandDefinition(sql, param, _session.Transaction ,cancellationToken: cancellationToken));

            using var conn = _connectionFactory.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<T>(
                new CommandDefinition(sql, param, cancellationToken: cancellationToken));
        }

        protected virtual async Task<T?> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? param = null,
            CancellationToken cancellationToken = default)
        {
            if (_session.Transaction is not null)
                return await _session.Transaction.Connection!.QueryFirstOrDefaultAsync<T>(
                new CommandDefinition(sql, param, _session.Transaction, cancellationToken: cancellationToken));

            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<T>(
                new CommandDefinition(sql, param, cancellationToken: cancellationToken));
        }


    }
}
