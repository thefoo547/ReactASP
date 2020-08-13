using App.ErrorHandlers;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Comments
{
    public class DeleteComment
    {
        public class Execute : IRequest
        {
            public Guid CommentId { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly AppDBContext appDBContext;

            public Handler(AppDBContext appDBContext)
            {
                this.appDBContext = appDBContext;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var comment = appDBContext.Comments.Where(x => x.CommentId == request.CommentId).FirstOrDefault();

                appDBContext.Comments.Remove(comment);

                var res = await appDBContext.SaveChangesAsync();

                return (res > 0) ? Unit.Value :
                    throw new BusinessException(System.Net.HttpStatusCode.InternalServerError, "No se pudo Eliminar");
            }
        }
    }
}
