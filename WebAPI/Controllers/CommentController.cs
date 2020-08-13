using App.Comments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class CommentController : BreveControllerBase
    {

        /// <summary>
        /// Creates a Comment.
        /// </summary>
        /// <remarks>
        /// Create a new comment for a determined course putting in the respective username
        /// </remarks>
        /// <param name="newc">The new comment</param>
        /// <returns>Created status code</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
