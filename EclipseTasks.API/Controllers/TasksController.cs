using EclipseTasks.Application.Security;
using EclipseTasks.Application.UseCases.Tasks.Commands.CreateTask;
using EclipseTasks.Application.UseCases.Tasks.Commands.DeleteTask;
using EclipseTasks.Application.UseCases.Tasks.Commands.UpdateTask;
using EclipseTasks.Application.UseCases.Tasks.Queries.ListTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseTasks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthentication _authentication;

        public TasksController(IMediator mediator, IAuthentication authentication)
        {
            _mediator = mediator;
            _authentication = authentication;
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> Get(Guid projectId)
        {
            return Ok(await _mediator.Send(new ListTasksQuery(projectId)));
        }

        [HttpPost("{projectId}")]
        public async Task<IActionResult> Post(Guid projectId, CreateTaskCommand command)
        {
            command.ProjectId = projectId;
            command.ActorKey = _authentication.GetUser().Email;

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> Put(Guid projectId, UpdateTaskCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{projectId}/{id}")]
        public async Task<IActionResult> Delete(Guid projectId, Guid id)
        {
            await _mediator.Send(new DeleteTaskCommand(id));
            return Ok();
        }
    }
}