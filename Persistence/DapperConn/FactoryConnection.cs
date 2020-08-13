using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Persistence.DapperConn
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection dbConnection;
        private readonly IOptions<ConnConfig> options;

        public FactoryConnection(IOptions<ConnConfig> options)
        {
            this.options = options;
        }

        public void CloseConnection()
        {
            if (dbConnection != null && dbConnection.State == ConnectionState.Open)
            {
                dbConnection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (dbConnection == null)
            {
                dbConnection = new SqlConnection(options.Value.DefaultConnection);
            }
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }
    }
}
