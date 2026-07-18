using Dapper;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : SqlDataAccessBase, ICustomerRepository
    {
        private const string InsertSql = """
            INSERT INTO Customers (Id, Name)
            VALUES (@Id, @Name);

            SELECT CAST(SCOPE_IDENTITY() AS int);
        """;

        private const string SelectAllSql = """
            SELECT * 
            FROM Customers
            """;

        private const string SelectByIdSql = """
            SELECT TOP (1) Id
            FROM Customers WHERE Id = @Id
            """;

        public CustomerRepository(
            IDbSession session,
            ISqlConnectionFactory connectionFactory) : base(session, connectionFactory) {}

        public async Task<int> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            var param = new
            {
                customer.Id,
                customer.Name
            };

            var customerId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

            if (customerId.HasValue)
                return customerId.Value;

            try
            {
                customerId = await ExecuteScalarAsync<int?>(InsertSql, param, cancellationToken);
                return customerId!.Value;
            }
            catch (SqlException ex) when (ex.Number is 2627 or 2601)
            {
                customerId = await ExecuteScalarAsync<int?>(SelectByIdSql, param, cancellationToken);

                if (customerId.HasValue)
                    throw;

                return customerId.Value;
            }
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await QueryAsync<Customer>(SelectAllSql, null, cancellationToken);        
            return products.AsList();
        }

        public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await QueryFirstOrDefaultAsync<Customer>(SelectByIdSql, new { Id = id }, cancellationToken);
    }
}
