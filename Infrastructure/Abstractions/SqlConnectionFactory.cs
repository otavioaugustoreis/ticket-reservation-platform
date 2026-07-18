using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Abstractions
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {

        private readonly SqlServerOptionsConfiguration _options;

        public SqlConnectionFactory(IOptions<SqlServerOptionsConfiguration> options)
            => _options = options.Value;

        public IDbConnection CreateConnection()
        {
            if(string.IsNullOrEmpty(_options.ConnectionString))
                throw new InvalidOperationException("Connection string is not configured.");
            
            return new SqlConnection(_options.ConnectionString);
        }
    }
}
