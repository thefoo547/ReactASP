using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistence.DapperConn
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection dbConnection;
        private readonly IOptions<ConnConfig> options;

        public FactoryConnection(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
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
                dbConnection = new SqlConnection(options.Value.ConnectionString);
            }
            if(dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }
    }
}
