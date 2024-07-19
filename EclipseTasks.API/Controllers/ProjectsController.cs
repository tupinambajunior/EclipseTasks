using EclipseTasks.Application.UseCases.Projects.Commands.CreateProject;
using EclipseTasks.Application.UseCases.Projects.Commands.DeleteProject;
using EclipseTasks.Application.UseCases.Projects.Queries.ListProjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseTasks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new ListProjectsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));
            return Ok();
        }
    }
}