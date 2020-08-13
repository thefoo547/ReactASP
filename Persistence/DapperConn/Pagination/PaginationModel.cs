using System.Collections.Generic;

namespace Persistence.DapperConn.Pagination
{
    public class PaginationModel
    {
        public List<IDictionary<string, object>> RecordList { get; set; }
        public int RecordCount { get; set; }
        public int PagesCount { get; set; }
    }
}
