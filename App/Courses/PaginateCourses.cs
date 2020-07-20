using MediatR;
using Persistence.DapperConn.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Courses
{
    public class PaginateCourses
    {
        public class Execute : IRequest<PaginationModel>
        {
            public string Title { get; set; }
            public int PageNo { get; set; }
            public int Qty { get; set; }
        }

        public class Handler : IRequestHandler<Execute, PaginationModel>
        {
            private readonly IPagination pagination;

            public Handler(IPagination pagination)
            {
                this.pagination = pagination;
            }

            public async Task<PaginationModel> Handle(Execute request, CancellationToken cancellationToken)
            {
                var sp = "sp_Courses_Pagination";
                var ordering = "Title";
                var paras = new Dictionary<string, object>();
                paras.Add("@CourseName", request.Title);
                return await pagination.GetPagination(sp, request.PageNo, request.Qty, paras, ordering);
            }
        }

    }

}
