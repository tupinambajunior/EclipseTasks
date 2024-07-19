using EclipseTasks.Application.Security;
using EclipseTasks.Application.UseCases.Tasks.Commands.CreateComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseTasks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthentication _authentication;

        public CommentsController(IMediator mediator, IAuthentication authentication)
        {
            _mediator = mediator;
            _authentication = authentication;
        }

        [HttpPost("{taskId}")]
        public async Task<IActionResult> Post(Guid taskId, PostArgs args)
        {
            var command = new CreateCommentCommand
            {
                TaskId = taskId,
                Comment = args.Comment,
                UserKey = _authentication.GetUser().Email
            };
            await _mediator.Send(command);
            return Ok();
        }

        public record PostArgs(string Comment);
    }
}