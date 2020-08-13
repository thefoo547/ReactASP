using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.DapperConn.Pagination
{
    public interface IPagination
    {
        Task<PaginationModel> GetPagination(string storedProcedure,
            int pageNo, int qty, IDictionary<string, object> filters, string ordering);
    }
}
