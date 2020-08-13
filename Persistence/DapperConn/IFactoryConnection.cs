using System.Data;

namespace Persistence.DapperConn
{
    public interface IFactoryConnection
    {
        void CloseConnection();

        IDbConnection GetConnection();
    }
}