using System.Data;

namespace Infrastructure.Abstractions
{
    public interface IDbSession
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}
