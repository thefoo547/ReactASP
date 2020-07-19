using App.Comments;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class CommentController : BreveControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewComment.Execute newc)
        {
            return await Mediator.Send(newc);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(DeleteComment.Execute del, Guid id)
        {
            del.CommentId = id;
            return await Mediator.Send(del);
        }
    }
}
