using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Persistence.DapperConn
{
    public interface IFactoryConnection
    {
        void CloseConnection();

        IDbConnection GetConnection();
    }
}