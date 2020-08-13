using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.DapperConn.Pagination
{
    public class PaginationRepo : IPagination
    {
        private readonly IFactoryConnection factoryConnection;

        public PaginationRepo(IFactoryConnection factoryConnection)
        {
            this.factoryConnection = factoryConnection;
        }

        public async Task<PaginationModel> GetPagination(string storedProcedure, int pageNo,
            int qty, IDictionary<string, object> filters, string ordering)
        {
            PaginationModel paginacionModel = new PaginationModel();
            List<IDictionary<string, object>> listaReporte = null;
            int totalRecords = 0;
            int totalPaginas = 0;
            try
            {
                var connection = factoryConnection.GetConnection();
                DynamicParameters parametros = new DynamicParameters();

                foreach (var param in filters)
                {
                    parametros.Add("@" + param.Key, param.Value);
                }

                parametros.Add("@PageNo", pageNo);
                parametros.Add("@Qty", qty);
                parametros.Add("@Ordering", ordering);

                parametros.Add("@RecordCount", totalRecords, DbType.Int32, ParameterDirection.Output);
                parametros.Add("@PagesCount", totalPaginas, DbType.Int32, ParameterDirection.Output);

                var result = await connection.QueryAsync(storedProcedure, parametros, commandType: CommandType.StoredProcedure);
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginacionModel.RecordList = listaReporte;
                paginacionModel.PagesCount = parametros.Get<int>("@PagesCount");
                paginacionModel.RecordCount = parametros.Get<int>("@RecordCount");

            }
            catch (Exception e)
            {
                throw new Exception("No se pudo ejecutar el procedimiento almacenado", e);
            }
            finally
            {
                factoryConnection.CloseConnection();
            }

            return paginacionModel;
        }
    }
}
